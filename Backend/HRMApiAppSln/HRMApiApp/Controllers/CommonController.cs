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
 

        [HttpGet("departments")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllDepartments(int idClient)
        {
            var dept = await _commonService.GetAllDepartment(idClient);
            return Ok(dept);
        }
        [HttpGet("designation")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllDesignations(int idClient)
        {
            var desig = await _commonService.GetAllDesignation(idClient);
            return Ok(desig);
        }
        [HttpGet("educationlevel")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllEducationLevels(int idClient)
        {
            var eduLevel = await _commonService.GetAllEducationLevel(idClient);
            return Ok(eduLevel);
        }
        [HttpGet("educationresults")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllEducationResults(int idClient)
        {
            var eduResult = await _commonService.GetAllEducationResult(idClient);
            return Ok(eduResult);
        }
        [HttpGet("employeetype")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllEmployeeTypes(int idClient)
        {
            var empType =await _commonService.GetAllEmployeeType(idClient);
            return Ok(empType); 
        }
        [HttpGet("gender")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllGenders(int idClient)
        {
            var gender = await _commonService.GetAllGender(idClient);
            return Ok(gender);
        }
        [HttpGet("jobtype")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllJobTypes(int idClient)
        {
            var jobType = await _commonService.GetAllJobType(idClient);
            return Ok(jobType);
        }
        [HttpGet("maritalstatus")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllMaritalStatus(int idClient)
        {
            var maritalStatus = await _commonService.GetAllMaritalStatus(idClient);
            return Ok(maritalStatus);
        }
        [HttpGet("relationship")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllRelationship(int idClient)
        {
            var relation = await _commonService.GetAllRelationship(idClient);
            return Ok(relation);
        }
        [HttpGet("religion")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllReligions(int idClient)
        {
            var religion = await _commonService.GetAllReligion(idClient);
            return Ok(religion);
        }
        [HttpGet("weeksoff")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllWeekOffs(int idClient)
        {
            var weeksOff = await _commonService.GetAllWeekOff(idClient);
            return Ok(weeksOff);
        }


    }
}
