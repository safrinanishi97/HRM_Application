using HRMApiApp.BLL;
using HRMApiApp.BLL.Interfaces;
using HRMApiApp.DTO;
using HRMApiApp.Models;
using HRMApiApp.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMApiApp.Controllers
{
    [Route("api/common")]
    [ApiController]
    public class CommonController(ICommonService CommonService) : ControllerBase
    {
 

        [HttpGet("departmentdropdown")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllDepartments([FromQuery] int idClient)
        {
            var depertment = await CommonService.GetAllDepartment(idClient);
            return Ok(depertment);
        }
        [HttpGet("designationdropdown")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllDesignations([FromQuery] int idClient)
        {
            var designation = await CommonService.GetAllDesignation(idClient);
            return Ok(designation);
        }
        [HttpGet("educationleveldropdown")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllEducationLevels([FromQuery] int idClient)
        {
            var educationLevel = await CommonService.GetAllEducationLevel(idClient);
            return Ok(educationLevel);
        }
        [HttpGet("educationExaminationdropdown")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllEducationExaminations([FromQuery] int idClient)
        {
            var educationExamination = await CommonService.GetAllEducationExamination(idClient);
            return Ok(educationExamination);
        }
        [HttpGet("educationresultdropdown")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllEducationResults([FromQuery] int idClient)
        {
            var educationResult = await CommonService.GetAllEducationResult(idClient);
            return Ok(educationResult);
        }
        [HttpGet("employeetypedropdown")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllEmployeeTypes([FromQuery] int idClient)
        {
            var employeeType =await CommonService.GetAllEmployeeType(idClient);
            return Ok(employeeType); 
        }
        [HttpGet("genderdropdown")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllGenders([FromQuery] int idClient)
        {
            var gender = await CommonService.GetAllGender(idClient);
            return Ok(gender);
        }
        [HttpGet("jobtypedropdown")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllJobTypes([FromQuery] int idClient)
        {
            var jobType = await CommonService.GetAllJobType(idClient);
            return Ok(jobType);
        }
        [HttpGet("maritalstatusdropdown")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllMaritalStatus([FromQuery] int idClient)
        {
            var maritalStatus = await CommonService.GetAllMaritalStatus(idClient);
            return Ok(maritalStatus);
        }
        [HttpGet("relationshipdropdown")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllRelationship([FromQuery] int idClient)
        {
            var relation = await CommonService.GetAllRelationship(idClient);
            return Ok(relation);
        }
        [HttpGet("religiondropdown")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllReligions([FromQuery] int idClient)
        {
            var religion = await CommonService.GetAllReligion(idClient);
            return Ok(religion);
        }
        [HttpGet("sectiondropdown")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllSections([FromQuery] int idClient)
        {
            var section = await CommonService.GetAllSection(idClient);
            return Ok(section);
        }
        [HttpGet("weeksoffdropdown")]
        public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetAllWeekOffs([FromQuery] int idClient)
        {
            var weeksOff = await CommonService.GetAllWeekOff(idClient);
            return Ok(weeksOff);
        }


    }
}
