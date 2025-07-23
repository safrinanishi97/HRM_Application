using HRMApiApp.DAL.Interfaces;
using HRMApiApp.Model;
using HRMApiApp.Models;
using HRMApiApp.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMApiApp.DAL
{
    public class CommonRepository(HanaHrmContext context) : ICommonRepository
    {

        public readonly HanaHrmContext _context = context;

        public async Task<List<CommonViewModel>> GetAllDepartment(CancellationToken cancellationToken)
        {
            var dept= await _context.Departments.Select(e => new CommonViewModel {
             Id = e.Id,
             Name=e.DepartName
            }).ToListAsync(cancellationToken);
            return dept;
        }


        public async Task<List<CommonViewModel>> GetAllDesignation(CancellationToken cancellationToken)
        {
            var desig= await _context.Designations.Select(e => new CommonViewModel
            {
                Id = e.Id,
                Name = e.DesignationName
            }).ToListAsync(cancellationToken);
            return desig;
        }

        public async Task<List<CommonViewModel>> GetAllEducationLevel(CancellationToken cancellationToken)
        {
            var eduLevel= await _context.EducationLevels.Select(e => new CommonViewModel
            {
                Id = e.Id,
                Name = e.EducationLevelName
            }).ToListAsync(cancellationToken);
            return eduLevel;
        }

        public async Task<List<CommonViewModel>> GetAllEducationResult(CancellationToken cancellationToken)
        {
            var eduResult= await _context.EducationResults.Select(e => new CommonViewModel
            {
                Id = e.Id,
                Name = e.ResultName
            }).ToListAsync(cancellationToken);
            return eduResult;
        }

        public async Task<List<CommonViewModel>> GetAllEmployeeType(CancellationToken cancellationToken)
        {
            var empType = await _context.EmployeeTypes.Select(e => new CommonViewModel
            {
                Id = e.Id,
                Name = e.TypeName
            }).ToListAsync(cancellationToken);
            return empType;
        }

        public async Task<List<CommonViewModel>> GetAllGender(CancellationToken cancellationToken)
        {
            var gender = await _context.Genders.Select(e => new CommonViewModel
            {
                Id = e.Id,
                Name = e.GenderName
            }).ToListAsync(cancellationToken);
            return gender;
        }

        public async Task<List<CommonViewModel>> GetAllJobType(CancellationToken cancellationToken)
        {
            var jobType = await _context.JobTypes.Select(e => new CommonViewModel
            {
                Id = e.Id,
                Name = e.JobTypeName
            }).ToListAsync(cancellationToken);
            return jobType;
        }

        public async Task<List<CommonViewModel>> GetAllMaritalStatus(CancellationToken cancellationToken)
        {
            var maritalStatus = await _context.MaritalStatuses.Select(e => new CommonViewModel
            {
                Id = e.Id,
                Name = e.MaritalStatusName
            }).ToListAsync(cancellationToken);
            return maritalStatus;

        }

        public async Task<List<CommonViewModel>> GetAllRelationship(CancellationToken cancellationToken)
        {
            var relation = await _context.Relationships.Select(e => new CommonViewModel
            {
                Id = e.Id,
                Name = e.RelationName
            }).ToListAsync(cancellationToken);
            return relation;
        }

        public async Task<List<CommonViewModel>> GetAllReligion(CancellationToken cancellationToken)
        {
            var religion = await _context.Religions.Select(e => new CommonViewModel
            {
                Id = e.Id,
                Name = e.ReligionName
            }).ToListAsync(cancellationToken);
            return religion;
        }

        public async Task<List<CommonViewModel>> GetAllWeekOff(CancellationToken cancellationToken)
        {
            var weekOff =await _context.WeekOffs.Select(e => new CommonViewModel
            {
                Id = e.Id,
                Name = e.WeekOffDay
            }).ToListAsync(cancellationToken);
            return weekOff;
        }
    }
}
