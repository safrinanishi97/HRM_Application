using HRMApiApp.DAL.Interfaces;
using HRMApiApp.Model;
using HRMApiApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMApiApp.DAL
{
    public class CommonRepository : ICommonRepository
    {

        public readonly HanaHrmContext _context;

        public CommonRepository(HanaHrmContext context)
        {
            _context = context;
        }

        public List<CommonViewModel> GetAllDepartment()
        {
            return _context.Departments.Select(e => new CommonViewModel {
             Id = e.Id,
             Name=e.DepartName
            }).ToList();
        }

        public List<CommonViewModel> GetAllDesignation()
        {
            return _context.Designations.Select(e => new CommonViewModel
            {
                Id = e.Id,
                Name = e.DesignationName
            }).ToList();
        }

        public List<CommonViewModel> GetAllEducationLevel()
        {
            return _context.EducationLevels.Select(e => new CommonViewModel
            {
                Id = e.Id,
                Name = e.EducationLevelName
            }).ToList();
        }

        public List<CommonViewModel> GetAllEducationResult()
        {
            return _context.EducationResults.Select(e => new CommonViewModel
            {
                Id = e.Id,
                Name = e.ResultName
            }).ToList();
        }

        public List<CommonViewModel> GetAllEmployeeType()
        {
            return _context.EmployeeTypes.Select(e => new CommonViewModel
            {
                Id = e.Id,
                Name = e.TypeName
            }).ToList();
        }

        public List<CommonViewModel> GetAllGender()
        {
            return _context.Genders.Select(e => new CommonViewModel
            {
                Id = e.Id,
                Name = e.GenderName
            }).ToList();
        }

        public List<CommonViewModel> GetAllJobType()
        {
            return _context.JobTypes.Select(e => new CommonViewModel
            {
                Id = e.Id,
                Name = e.JobTypeName
            }).ToList();
        }

        public List<CommonViewModel> GetAllMaritalStatus()
        {
            return _context.MaritalStatuses.Select(e => new CommonViewModel
            {
                Id = e.Id,
                Name = e.MaritalStatusName
            }).ToList();
        }

        public List<CommonViewModel> GetAllRelationship()
        {
            return _context.Relationships.Select(e => new CommonViewModel
            {
                Id = e.Id,
                Name = e.RelationName
            }).ToList();
        }

        public List<CommonViewModel> GetAllReligion()
        {
            return _context.Religions.Select(e => new CommonViewModel
            {
                Id = e.Id,
                Name = e.ReligionName
            }).ToList();
        }

        public List<CommonViewModel> GetAllWeekOff()
        {
            return _context.WeekOffs.Select(e => new CommonViewModel
            {
                Id = e.Id,
                Name = e.WeekOffDay
            }).ToList();
        }
    }
}
