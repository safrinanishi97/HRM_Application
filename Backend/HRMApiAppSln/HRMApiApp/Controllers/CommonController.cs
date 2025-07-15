using HRMApiApp.BLL;
using HRMApiApp.BLL.Interfaces;
using HRMApiApp.Models;
using HRMApiApp.ViewModel;
using HRMApiApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ICommonService _commonService;

        public CommonController(ICommonService commonService)
        {
            _commonService = commonService;
        }

        [HttpGet("departments")]
        public ActionResult<List<Department>> GetAllDepartments()
        {
            var dept = _commonService.GetAllDepartment();
            return Ok(dept);
        }
        [HttpGet("designation")]
        public ActionResult<List<Designation>> GetAllDesignations()
        {
            var desig = _commonService.GetAllDesignation();
            return Ok(desig);
        }
        [HttpGet("educationLevel")]
        public ActionResult<List<EducationLevel>> GetAllEducationLevels()
        {
            var eduLevel = _commonService.GetAllEducationLevel();
            return Ok(eduLevel);
        }
        [HttpGet("educationResults")]
        public ActionResult<List<EducationResult>> GetAllEducationResults()
        {
            var eduResult = _commonService.GetAllEducationResult();
            return Ok(eduResult);
        }
        [HttpGet("employeeType")]
        public ActionResult<List<EmployeeType>> GetAllEmployeeTypes()
        {
            var empType = _commonService.GetAllEmployeeType();
            return Ok(empType);
        }
        [HttpGet("gender")]
        public ActionResult<List<Gender>> GetAllGenders()
        {
            var gender = _commonService.GetAllGender();
            return Ok(gender);
        }
        [HttpGet("jobType")]
        public ActionResult<List<JobType>> GetAllJobTypes()
        {
            var jobType = _commonService.GetAllJobType();
            return Ok(jobType);
        }
        [HttpGet("maritalStatus")]
        public ActionResult<List<MaritalStatus>> GetAllMaritalStatus()
        {
            var maritalStatus = _commonService.GetAllMaritalStatus();
            return Ok(maritalStatus);
        }
        [HttpGet("relationship")]
        public ActionResult<List<Relationship>> GetAllRelationship()
        {
            var relation = _commonService.GetAllRelationship();
            return Ok(relation);
        }
        [HttpGet("religion")]
        public ActionResult<List<Religion>> GetAllReligions()
        {
            var religion = _commonService.GetAllReligion();
            return Ok(religion);
        }
        [HttpGet("weeksOff")]
        public ActionResult<List<Employee>> GetAllWeekOffs()
        {
            var weeksOff = _commonService.GetAllWeekOff();
            return Ok(weeksOff);
        }


    }
}
