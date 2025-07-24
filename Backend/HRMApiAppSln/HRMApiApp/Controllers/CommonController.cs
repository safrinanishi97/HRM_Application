using HRMApiApp.BLL;
using HRMApiApp.BLL.Interfaces;
using HRMApiApp.DTO;
using HRMApiApp.Models;
using HRMApiApp.ViewModel;
using HRMApiApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMApiApp.Controllers
{
    [Route("api/common")]
    [ApiController]
    public class CommonController(ICommonService _commonService) : ControllerBase
    {
 

        [HttpGet("department")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllDepartments([FromQuery] int idClient)
        {
            var depertment = await _commonService.GetAllDepartment(idClient);
            return Ok(depertment);
        }
        [HttpGet("designation")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllDesignations([FromQuery] int idClient)
        {
            var designation = await _commonService.GetAllDesignation(idClient);
            return Ok(designation);
        }
        [HttpGet("educationlevel")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllEducationLevels([FromQuery] int idClient)
        {
            var educationLevel = await _commonService.GetAllEducationLevel(idClient);
            return Ok(educationLevel);
        }
        [HttpGet("educationresult")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllEducationResults([FromQuery] int idClient)
        {
            var educationResult = await _commonService.GetAllEducationResult(idClient);
            return Ok(educationResult);
        }
        [HttpGet("employeetype")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllEmployeeTypes([FromQuery] int idClient)
        {
            var employeeType =await _commonService.GetAllEmployeeType(idClient);
            return Ok(employeeType); 
        }
        [HttpGet("gender")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllGenders([FromQuery] int idClient)
        {
            var gender = await _commonService.GetAllGender(idClient);
            return Ok(gender);
        }
        [HttpGet("jobtype")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllJobTypes([FromQuery] int idClient)
        {
            var jobType = await _commonService.GetAllJobType(idClient);
            return Ok(jobType);
        }
        [HttpGet("maritalstatus")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllMaritalStatus([FromQuery] int idClient)
        {
            var maritalStatus = await _commonService.GetAllMaritalStatus(idClient);
            return Ok(maritalStatus);
        }
        [HttpGet("relationship")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllRelationship([FromQuery] int idClient)
        {
            var relation = await _commonService.GetAllRelationship(idClient);
            return Ok(relation);
        }
        [HttpGet("religion")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllReligions([FromQuery] int idClient)
        {
            var religion = await _commonService.GetAllReligion(idClient);
            return Ok(religion);
        }
        [HttpGet("weeksoff")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllWeekOffs([FromQuery] int idClient)
        {
            var weeksOff = await _commonService.GetAllWeekOff(idClient);
            return Ok(weeksOff);
        }


    }
}
