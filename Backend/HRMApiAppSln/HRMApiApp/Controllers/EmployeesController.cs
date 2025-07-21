using HRMApiApp.BLL.Interfaces;
using HRMApiApp.DTO;
using HRMApiApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace HRMApiApp.Controllers
{
    //    [Route("api//{idClient}/employees")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("allemployees")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAllEmployees(CancellationToken cancellationToken)
        {
            var employees = await _employeeService.GetAllAsync(cancellationToken);
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById([FromQuery] int idClient, int id, CancellationToken cancellationToken)
        {
            var employee = await _employeeService.GetByIdAsync(idClient, id, cancellationToken);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost("createemployeewithdetails")]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeCreateDTO employeeDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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

        [HttpPut("{idClient}/{id}")]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeUpdateDTO employeeDto, CancellationToken cancellationToken)
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

        [HttpDelete("{idClient}/{id}")]
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
