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
    public class EmployeesController(IEmployeeService _employeeService, IValidator<EmployeeCreateDTO> _validator) : ControllerBase
    {    
        [HttpGet("allemployees")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAllEmployees([FromQuery] int idClient)
        {
            var employees = await _employeeService.GetAllAsync(idClient);
            return Ok(employees);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetEmployeeById([FromQuery]int idClient, [FromQuery] int id)
        {
            var employee = await _employeeService.GetByIdAsync(idClient, id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

      

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromForm] EmployeeCreateDTO employeeDto)
        {
            var validationResult = await _validator.ValidateAsync(employeeDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors
                    .Select(e => e.ErrorMessage));
            }
          
            var success = await _employeeService.CreateAsync(employeeDto);

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
        public async Task<IActionResult> UpdateEmployee([FromForm] EmployeeUpdateDTO employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _employeeService.UpdateAsync(employeeDto);

            return result switch
            {
                "Success" => Ok("Employee updated successfully"),
                "Employee not found" => NotFound("Employee not found"),
                var error when error.StartsWith("Error:") => StatusCode(500, error),
                _ => StatusCode(500, "Unexpected error")
            };
        }

        [HttpDelete("{idClient}/{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int idClient, [FromRoute] int id)
        {
            var result = await _employeeService.DeleteAsync(idClient, id);
            if (!result)
            {
                return NotFound("Employee not found.");
            }

            return Ok(new { message = "Employee soft deleted successfully." });
        }

        [HttpGet("file")]
        public async Task<IActionResult> GetEmployeeFile([FromQuery] int idClient, [FromQuery] int id, [FromQuery] string fileType, [FromQuery] int? documentId)
        {
            var (fileData, mimeType) = await _employeeService.GetEmployeeFileAsync(idClient, id, fileType, documentId);

            if (fileData == null || string
                .IsNullOrEmpty(mimeType))
                return NotFound();

            return File(fileData, mimeType);
        }
    }
}
