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

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController(IEmployeeService employeeService, IValidator<EmployeeCreateDTO> validator) : ControllerBase
    {
        private readonly IEmployeeService _employeeService = employeeService;
        private readonly IValidator<EmployeeCreateDTO> _validator = validator;
         
        [HttpGet("allemployees")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAllEmployees(CancellationToken cancellationToken)
        {
            var employees = await _employeeService.GetAllAsync(cancellationToken);
            return Ok(employees);
        }

        [HttpGet("getbyid/{idClient:int}/{id:int}")]
        public async Task<IActionResult> GetEmployeeById(int idClient, int id, CancellationToken cancellationToken)
        {
            var employee = await _employeeService.GetByIdAsync(idClient, id, cancellationToken);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpGet("file/{idClient:int}/{id:int}")]
        public async Task<IActionResult> GetEmployeeFile(int idClient, int id,string fileType,int? documentId,CancellationToken cancellationToken)
        {
            var (fileData, mimeType) = await _employeeService.GetEmployeeFileAsync(idClient, id, fileType, documentId, cancellationToken);

            if (fileData == null || string.IsNullOrEmpty(mimeType))
                return NotFound();

            return File(fileData, mimeType);
        }

        [HttpPost("createemployeewithdetails")]
        public async Task<IActionResult> CreateEmployee([FromForm] EmployeeCreateDTO employeeDto, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(employeeDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }
            if (employeeDto.ProfileImage == null)
            {
                Console.WriteLine("ProfileImage is NULL");
            }
            else
            {
                Console.WriteLine($"ProfileImage: {employeeDto.ProfileImage.FileName}");
            }

            foreach (var doc in employeeDto.Documents)
            {
                if (doc.UpFile == null)
                    Console.WriteLine($"Document [{doc.DocumentName}] File is NULL");
                else
                    Console.WriteLine($"Document [{doc.DocumentName}] File Name: {doc.UpFile.FileName}");
            }
            var success = await _employeeService.CreateAsync(employeeDto, cancellationToken);

            if (success)
                return Created("", new
                {
                    Success = true,
                    Message = "Employee created successfully"
                });
            else
                return BadRequest("Failed to create employee");
        }

        [HttpPut("updateemployee/{idClient}/{id}")]
        public async Task<IActionResult> UpdateEmployee([FromForm] EmployeeUpdateDTO employeeDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _employeeService.UpdateAsync(employeeDto, cancellationToken);

            return result switch
            {
                "Success" => Ok("Employee updated successfully"),
                "Employee not found" => NotFound("Employee not found"),
                var error when error.StartsWith("Error:") => StatusCode(500, error),
                _ => StatusCode(500, "Unexpected error")
            };
        }

        [HttpDelete("deleteemployee/{idClient}/{id}")]
        public async Task<IActionResult> DeleteEmployee(int idClient, int id, CancellationToken cancellationToken)
        {
            var result = await _employeeService.DeleteAsync(id, idClient, cancellationToken);
            if (!result)
            {
                return NotFound("Employee not found.");
            }

            return Ok(new { message = "Employee soft deleted successfully." });
        }
    }
}
