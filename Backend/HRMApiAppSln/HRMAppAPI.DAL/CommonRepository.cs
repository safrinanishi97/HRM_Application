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
    public class CommonRepository(HanaHrmContext Context) : ICommonRepository
    {
        public async Task<List<CommonViewModel>> GetAllDepartment(int idClient)
        {
         
            var dept= await Context.Departments
                .AsNoTracking()
                .Where(d=>d.IdClient==idClient)
                .Select(e => new CommonViewModel 
            {
                Id = e.Id,
                Text = e.DepartName
            }).ToListAsync();
            return dept;
        }
      

        public async Task<List<CommonViewModel>> GetAllDesignation(int idClient)
        {
            var desig= await Context.Designations
                .AsNoTracking()
                .Where(d => d.IdClient == idClient)
                .Select(e => new CommonViewModel
            {
                Id = e.Id,
                Text = e.DesignationName
            }).ToListAsync();
            return desig;
        }

        public async Task<List<CommonViewModel>> GetAllEducationExamination(int idClient)
        {
            var eduExam = await Context.EducationExaminations
                .AsNoTracking()
                 .Where(d => d.IdClient == idClient)
                .Select(e => new CommonViewModel
                {
                    Id = e.Id,
                    Text = e.ExamName
                }).ToListAsync();
            return eduExam;
        }

        public async Task<List<CommonViewModel>> GetAllEducationLevel(int idClient)
        {
            var eduLevel= await Context.EducationLevels
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
            var eduResult= await Context.EducationResults
                .AsNoTracking()
                 .Where(d => d.IdClient == idClient)
                .Select(e => new CommonViewModel
            {
                Id = e.Id,
                Text = e.ResultName
            }).ToListAsync();
            return eduResult;
        }

        public async Task<List<CommonViewModel>> GetAllEmployeeType(int idClient)
        {
            var empType = await Context.EmployeeTypes
                .AsNoTracking()
                 .Where(d => d.IdClient == idClient)
                .Select(e => new CommonViewModel
            {
                Id = e.Id,
                Text = e.TypeName ?? ""
            }).ToListAsync();
            return empType;
        }

        public async Task<List<CommonViewModel>> GetAllGender(int idClient)
        {
            var gender = await Context.Genders
                .AsNoTracking()
                 .Where(d => d.IdClient == idClient)
                .Select(e => new CommonViewModel
            {
                Id = e.Id,
                Text = e.GenderName ?? ""
            }).ToListAsync();
            return gender;
        }

        public async Task<List<CommonViewModel>> GetAllJobType(int idClient)
        {
            var jobType = await Context.JobTypes
                .AsNoTracking()
                 .Where(d => d.IdClient == idClient)
                .Select(e => new CommonViewModel
            {
                Id = e.Id,
                Text = e.JobTypeName
            }).ToListAsync();
            return jobType;
        }

        public async Task<List<CommonViewModel>> GetAllMaritalStatus(int idClient)
        {
            var maritalStatus = await Context.MaritalStatuses
                .AsNoTracking()
                 .Where(d => d.IdClient == idClient)
                .Select(e => new CommonViewModel
            {
                Id = e.Id,
                Text = e.MaritalStatusName
            }).ToListAsync();
            return maritalStatus;

        }

        public async Task<List<CommonViewModel>> GetAllRelationship(int idClient)
        {
            var relation = await Context.Relationships
                .AsNoTracking()
                 .Where(d => d.IdClient == idClient)
                .Select(e => new CommonViewModel
            {
                Id = e.Id,
                Text = e.RelationName
            }).ToListAsync();
            return relation;
        }

        public async Task<List<CommonViewModel>> GetAllReligion(int idClient)
        {
            var religion = await Context.Religions
                .AsNoTracking()
                 .Where(d => d.IdClient == idClient)
                .Select(e => new CommonViewModel
            {
                Id = e.Id,
                Text = e.ReligionName
            }).ToListAsync();
            return religion;
        }

        public async Task<List<CommonViewModel>> GetAllSection(int idClient)
        {
            var section = await Context.Sections
                .AsNoTracking()
                .Where(d=>d.IdClient == idClient)
                .Select(e=> new CommonViewModel
                { Id = e.Id,
                Text = e.SectionName ?? ""
                }).ToListAsync();
            return section;
        }

        public async Task<List<CommonViewModel>> GetAllWeekOff(int idClient)
        {
            var weekOff =await Context.WeekOffs
                .AsNoTracking()
                 .Where(d => d.IdClient == idClient)
                .Select(e => new CommonViewModel
            {
                Id = e.Id,
                Text = e.WeekOffDay ?? ""
            }).ToListAsync();
            return weekOff;
        }
    }
}
