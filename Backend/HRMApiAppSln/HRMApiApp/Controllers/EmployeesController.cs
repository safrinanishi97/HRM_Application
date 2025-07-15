using HRMApiApp.BLL.Interfaces;
using HRMApiApp.DTO;
using HRMApiApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace HRMApiApp.Controllers
{
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
        public IActionResult GetEmployeeById(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public ActionResult<bool> AddEmployee([FromBody] EmployeeDTO empDto) {

            bool emp = _employeeService.AddEmployee(empDto);
            if (emp) return Ok("Employee added successfully");
            return Ok(false);
        }

        [HttpPut("{id}")]
        public ActionResult<bool> UpdateEmployee(int id,[FromBody] EmployeeDTO empDto)
        {
            //if (id != empDto.Id)
            //{
            //    return BadRequest("ID mismatch");
            //}

            bool emp = _employeeService.UpdateEmployee(empDto);
            if (emp) return Ok("Employee updated successfully");
            return Ok(false);
            //return result ? Ok(true) : BadRequest("Update failed");
        }

        [HttpDelete("{id}")]
        public ActionResult <bool> DeleteEmployee(int id)
        {
            var result = _employeeService.DeleteEmployee(id);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }  
   }
}
