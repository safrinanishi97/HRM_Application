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
            return _context.Employees.Select(e=>new EmployeeViewModel
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

        }
        public Employee GetEmployeeById(int id)
        {
            return _context.Employees.FirstOrDefault(e => e.Id == id) ?? throw new InvalidOperationException($"Employee with ID {id} not found");
        }
        public bool AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            var result = _context.SaveChanges();
            if(result> 0) return true;
            return false;
        }

        public bool UpdateEmployee(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            var result = _context.SaveChanges();
            if (result > 0) return true;
            return false;
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _context.Employees.FirstOrDefault(s=>s.Id==id);
            if (employee == null)
            {
                return false; 
            }
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return true;

        }
    }
}
