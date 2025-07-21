using HRMApiApp.DAL.Interfaces;
using HRMApiApp.Model;
using HRMApiApp.Models;
using HRMApiApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HRMApiApp.DAL
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public readonly HanaHrmContext _context;

        public EmployeeRepository(HanaHrmContext context)
        {
            _context = context;
        }

        public List<EmployeeViewModel> GetAllEmployees()
        {
            var employees = _context.Employees.Select(e => new EmployeeViewModel
            {
                Id = e.Id,
                IdClient = e.IdClient,
                EmployeeName = e.EmployeeName,
                EmployeeNameBangla = e.EmployeeNameBangla,
                //EmployeeImage = e.EmployeeImage,
                FatherName = e.FatherName,
                MotherName = e.MotherName,
                IdReportingManager = e.IdReportingManager,
                IdJobType = e.IdJobType,
                IdEmployeeType = e.IdEmployeeType,
                BirthDate = e.BirthDate,
                JoiningDate = e.JoiningDate,
                IdGender = e.IdGender,
                IdReligion = e.IdReligion,
                IdDepartment = e.IdDepartment,
                DepartName = e.Department.DepartName,
                SectionName = e.Section.SectionName,
                JobTypeName = e.JobType.JobTypeName,
                JobTypeBanglaName = e.JobType.JobTypeBanglaName,
                GenderName = e.Gender.GenderName,
                DepartNameBangla = e.Department.DepartNameBangla,
                DesignationNameBangla = e.Designation.DesignationNameBangla,
                ReligionName = e.Religion.ReligionName,
                SectionNameBangla = e.Section.SectionNameBangla,
                DesignationName = e.Designation.DesignationName,
                WeekOffDay = e.WeekOff.WeekOffDay,
                MaritalStatusName = e.MaritalStatus.MaritalStatusName,
                IdSection = e.IdSection,
                IdDesignation = e.IdDesignation,
                HasOvertime = e.HasOvertime,
                HasAttendenceBonus = e.HasAttendenceBonus,
                IdWeekOff = e.IdWeekOff,
                Address = e.Address,
                PresentAddress = e.PresentAddress,
                NationalIdentificationNumber = e.NationalIdentificationNumber,
                ContactNo = e.ContactNo,
                IdMaritalStatus = e.IdMaritalStatus,
                IsActive = e.IsActive,
                SetDate = e.SetDate,
                CreatedBy = e.CreatedBy
            }
            ).ToList();
            return employees;
        }
        public async Task<Employee?> GetEmployeeById(int idClient, int id)
        {
            return await _context.Employees
        .FirstOrDefaultAsync(e => e.IdClient == idClient &&  e.Id == id);
        }
        //public bool AddEmployee(Employee employee)
        //{
        //    _context.Employees.Add(employee);
        //    var result = _context.SaveChanges();
        //    if(result> 0) return true;
        //    return false;
        //}

        public async Task<Employee?> AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            var save = await _context.SaveChangesAsync();
            return save > 0 ? employee : null;
        }
        public async Task<Employee?> GetEmployeeForUpdate(int idClient, int id)
        {
            return await _context.Employees
                .AsTracking() 
                .FirstOrDefaultAsync(e => e.IdClient == idClient && e.Id == id);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }


        //Hard delete
        //public bool DeleteEmployee(int id)
        //{
        //    var employee = _context.Employees.FirstOrDefault(s=>s.Id==id);
        //    if (employee == null)
        //    {
        //        return false; 
        //    }
        //    _context.Employees.Remove(employee);
        //    _context.SaveChanges();
        //    return true;

        //}

        //soft delete
        public async Task<bool> DeleteEmployee(int id, int idClient) { 
        var employee =  await _context.Employees.FirstOrDefaultAsync(e => e.IdClient == 10001001 && e.Id == id);
            // FirstOrDefaultAsync(e => e.IdClient== idClient && e.Id == id)

            if (employee == null)
            {
                return false;
            }
            employee.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

       
    }
}
