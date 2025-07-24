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
    public class CommonRepository(HanaHrmContext _context) : ICommonRepository
    {
        public async Task<List<CommonViewModel>> GetAllDepartment(int idClient)
        {
            var cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            var dept= await _context.Departments
                .AsNoTracking()
                .Where(d=>d.IdClient==idClient)
                .Select(e => new CommonViewModel 
            {
                Id = e.Id,
                Text = e.DepartName
            }).ToListAsync(token);
            return dept;
        }
      

        public async Task<List<CommonViewModel>> GetAllDesignation(int idClient)
        {
            var cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            var desig= await _context.Designations
                .AsNoTracking()
                .Where(d => d.IdClient == idClient)
                .Select(e => new CommonViewModel
            {
                Id = e.Id,
                Text = e.DesignationName
            }).ToListAsync(token);
            return desig;
        }

        public async Task<List<CommonViewModel>> GetAllEducationLevel(int idClient)
        {
            var eduLevel= await _context.EducationLevels
                .AsNoTracking()
                 .Where(d => d.IdClient == idClient)
                .Select(e => new CommonViewModel
            {
                Id = e.Id,
                Text = e.EducationLevelName
            }).ToListAsync();
            return eduLevel;
        }

        public async Task<List<CommonViewModel>> GetAllEducationResult(int idClient)
        {
            var cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            var eduResult= await _context.EducationResults
                .AsNoTracking()
                 .Where(d => d.IdClient == idClient)
                .Select(e => new CommonViewModel
            {
                Id = e.Id,
                Text = e.ResultName
            }).ToListAsync(token);
            return eduResult;
        }

        public async Task<List<CommonViewModel>> GetAllEmployeeType(int idClient)
        {
            var cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            var empType = await _context.EmployeeTypes
                .AsNoTracking()
                 .Where(d => d.IdClient == idClient)
                .Select(e => new CommonViewModel
            {
                Id = e.Id,
                Text = e.TypeName ?? ""
            }).ToListAsync(token);
            return empType;
        }

        public async Task<List<CommonViewModel>> GetAllGender(int idClient)
        {
            var cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            var gender = await _context.Genders
                .AsNoTracking()
                 .Where(d => d.IdClient == idClient)
                .Select(e => new CommonViewModel
            {
                Id = e.Id,
                Text = e.GenderName ?? ""
            }).ToListAsync(token);
            return gender;
        }

        public async Task<List<CommonViewModel>> GetAllJobType(int idClient)
        {
            var cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            var jobType = await _context.JobTypes
                .AsNoTracking()
                 .Where(d => d.IdClient == idClient)
                .Select(e => new CommonViewModel
            {
                Id = e.Id,
                Text = e.JobTypeName
            }).ToListAsync(token);
            return jobType;
        }

        public async Task<List<CommonViewModel>> GetAllMaritalStatus(int idClient)
        {
            var cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            var maritalStatus = await _context.MaritalStatuses
                .AsNoTracking()
                 .Where(d => d.IdClient == idClient)
                .Select(e => new CommonViewModel
            {
                Id = e.Id,
                Text = e.MaritalStatusName
            }).ToListAsync(token);
            return maritalStatus;

        }

        public async Task<List<CommonViewModel>> GetAllRelationship(int idClient)
        {
            var cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            var relation = await _context.Relationships
                .AsNoTracking()
                 .Where(d => d.IdClient == idClient)
                .Select(e => new CommonViewModel
            {
                Id = e.Id,
                Text = e.RelationName
            }).ToListAsync(token);
            return relation;
        }

        public async Task<List<CommonViewModel>> GetAllReligion(int idClient)
        {
            var cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            var religion = await _context.Religions
                .AsNoTracking()
                 .Where(d => d.IdClient == idClient)
                .Select(e => new CommonViewModel
            {
                Id = e.Id,
                Text = e.ReligionName
            }).ToListAsync(token);
            return religion;
        }

        public async Task<List<CommonViewModel>> GetAllWeekOff(int idClient)
        {
            var cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            var weekOff =await _context.WeekOffs
                .AsNoTracking()
                 .Where(d => d.IdClient == idClient)
                .Select(e => new CommonViewModel
            {
                Id = e.Id,
                Text = e.WeekOffDay ?? ""
            }).ToListAsync(token);
            return weekOff;
        }
    }
}
