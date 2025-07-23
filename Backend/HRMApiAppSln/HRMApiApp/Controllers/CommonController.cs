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
    public class CommonController(ICommonService commonService) : ControllerBase
    {
        private readonly ICommonService _commonService = commonService;

        [HttpGet("departments")]
        public ActionResult<List<Department>> GetAllDepartments(CancellationToken cancellationToken)
        {
            var dept = _commonService.GetAllDepartment(cancellationToken);
            return Ok(dept);
        }
        [HttpGet("designation")]
        public ActionResult<List<Designation>> GetAllDesignations(CancellationToken cancellationToken)
        {
            var desig = _commonService.GetAllDesignation(cancellationToken);
            return Ok(desig);
        }
        [HttpGet("educationLevel")]
        public ActionResult<List<EducationLevel>> GetAllEducationLevels(CancellationToken cancellationToken)
        {
            var eduLevel = _commonService.GetAllEducationLevel(cancellationToken);
            return Ok(eduLevel);
        }
        [HttpGet("educationResults")]
        public ActionResult<List<EducationResult>> GetAllEducationResults(CancellationToken cancellationToken)
        {
            var eduResult = _commonService.GetAllEducationResult(cancellationToken);
            return Ok(eduResult);
        }
        [HttpGet("employeeType")]
        public ActionResult<List<EmployeeType>> GetAllEmployeeTypes(CancellationToken cancellationToken)
        {
            var empType = _commonService.GetAllEmployeeType(cancellationToken);
            return Ok(empType);
        }
        [HttpGet("gender")]
        public ActionResult<List<Gender>> GetAllGenders(CancellationToken cancellationToken)
        {
            var gender = _commonService.GetAllGender(cancellationToken);
            return Ok(gender);
        }
        [HttpGet("jobType")]
        public ActionResult<List<JobType>> GetAllJobTypes(CancellationToken cancellationToken)
        {
            var jobType = _commonService.GetAllJobType(cancellationToken);
            return Ok(jobType);
        }
        [HttpGet("maritalStatus")]
        public ActionResult<List<MaritalStatus>> GetAllMaritalStatus(CancellationToken cancellationToken)
        {
            var maritalStatus = _commonService.GetAllMaritalStatus(cancellationToken);
            return Ok(maritalStatus);
        }
        [HttpGet("relationship")]
        public ActionResult<List<Relationship>> GetAllRelationship(CancellationToken cancellationToken)
        {
            var relation = _commonService.GetAllRelationship(cancellationToken);
            return Ok(relation);
        }
        [HttpGet("religion")]
        public ActionResult<List<Religion>> GetAllReligions(CancellationToken cancellationToken)
        {
            var religion = _commonService.GetAllReligion(cancellationToken);
            return Ok(religion);
        }
        [HttpGet("weeksOff")]
        public ActionResult<List<Employee>> GetAllWeekOffs(CancellationToken cancellationToken)
        {
            var weeksOff = _commonService.GetAllWeekOff(cancellationToken);
            return Ok(weeksOff);
        }


    }
}
