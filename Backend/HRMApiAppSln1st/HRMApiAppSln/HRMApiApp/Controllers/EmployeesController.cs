using HRMApiApp.BLL.Interfaces;
using HRMApiApp.DTO;
using HRMApiApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace HRMApiApp.Controllers
{
    //    [Route("api/clients/{idClient}/employees")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public ActionResult<List<Employee>> GetAll()
        {
            var employees = _employeeService.GetAllEmployees();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById( [FromQuery] int idClient, int id)
        {
            var employee = await _employeeService.GetEmployeeById(idClient, id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }
        [HttpPost("create-with-details")]
        public async Task<IActionResult> CreateEmployeeWithDetails([FromBody] EmployeeDetailDTO employeeDetail)
        {
            if (employeeDetail == null)
                return BadRequest("Invalid employee data");

            var result = await _employeeService.CreateEmployeeWithDetails(employeeDetail);

            if (result == null)
                return StatusCode(500, "Failed to create employee");

            return Ok(new
            {
                message = "Employee created successfully",
                employee = result
            }); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeDTO dto)
        {
            dto.Id = id;
            var updated = await _employeeService.UpdateEmployee(dto);
            return updated ? Ok("Updated successfully") : NotFound("Employee not found");
        }

        //[HttpDelete("{id}")]
        //public ActionResult <bool> DeleteEmployee(int id)
        //{
        //    var result = _employeeService.DeleteEmployee(id);

        //    if (result)
        //    {
        //        return NoContent();
        //    }

        //    return NotFound();
        //}  

        [HttpDelete("{idClient}/{id}")]
        public async Task<IActionResult> SoftDeleteEmployee(int idClient, int id)
        {
            var success = await _employeeService.DeleteEmployee(id, idClient);
            if (!success)
                return NotFound("Employee not found.");

            return Ok(new { message = "Employee soft deleted successfully." });
        }
    }
}
