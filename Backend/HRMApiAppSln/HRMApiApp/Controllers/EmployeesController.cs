using FluentValidation;
using HRMApiApp.BLL.Interfaces;
using HRMApiApp.DTO;
using HRMApiApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace HRMApiApp.Controllers
{

    [Route("api/employee")]
    [ApiController]
    public class EmployeesController(IEmployeeService EmployeeService) : ControllerBase
    {    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAllEmployees([FromQuery] int idClient,CancellationToken cancellationToken)
        {
            var employees = await EmployeeService.GetAllAsync(idClient,cancellationToken);
            return Ok(employees);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetEmployeeById([FromQuery]int idClient, [FromQuery] int id, CancellationToken cancellationToken)
        {
            var employee = await EmployeeService.GetByIdAsync(idClient, id,cancellationToken);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }


        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromForm] EmployeeCreateDTO employeeDto, CancellationToken cancellationToken)
        {
          
            var success = await EmployeeService.CreateAsync(employeeDto, cancellationToken);

            if (success)
                return Created("", new
                {
                    Success = true,
                    Message = "Employee created successfully"
                });
            else
                return BadRequest("Failed to create employee");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromForm] EmployeeUpdateDTO employeeDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await EmployeeService.UpdateAsync(employeeDto, cancellationToken);

            return Ok(new
            {
                success = result == "Success",
                message = result
            });
        }


        [HttpDelete("{idClient}/{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int idClient, [FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await EmployeeService.DeleteAsync(idClient, id,cancellationToken);
            if (!result)
            {
                return NotFound("Employee not found.");
            }

            return Ok(new { message = "Employee soft deleted successfully." });
        }

        //[HttpGet("getfile")]
        //public async Task<IActionResult> GetEmployeeFile([FromQuery] int idClient, [FromQuery] int id, [FromQuery] string fileType, [FromQuery] int? documentId)
        //{
        //    var (fileData, mimeType) = await _employeeService.GetEmployeeFileAsync(idClient, id, fileType, documentId);

        //    if (fileData == null || string
        //        .IsNullOrEmpty(mimeType))
        //        return NotFound();

        //    return File(fileData, mimeType);
        //}
    }
}
