using HRMApiApp.BLL.Interfaces;
using HRMApiApp.DAL.Interfaces;
using HRMApiApp.DTO;
using HRMApiApp.Models;
using HRMApiApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMApiApp.BLL
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public List<EmployeeViewModel> GetAllEmployees()

        {
            return _employeeRepository.GetAllEmployees();
        }
        public Employee GetEmployeeById(int id)
        {
            return _employeeRepository.GetEmployeeById(id);
        }
        public bool AddEmployee(EmployeeDTO employeeDTO)
        {
            var employee = new Employee
            {
                IdClient = employeeDTO.IdClient,
                EmployeeName = employeeDTO.EmployeeName,
                EmployeeNameBangla = employeeDTO.EmployeeNameBangla,
                //EmployeeImage = employeeDTO.EmployeeImage,
                FatherName = employeeDTO.FatherName,
                MotherName = employeeDTO.MotherName,
                IdReportingManager = employeeDTO.IdReportingManager,
                IdJobType = employeeDTO.IdJobType,
                IdEmployeeType = employeeDTO.IdEmployeeType,
                BirthDate = employeeDTO.BirthDate,
                JoiningDate = employeeDTO.JoiningDate,
                IdGender = employeeDTO.IdGender,
                IdReligion = employeeDTO.IdReligion,
                IdDepartment = employeeDTO.IdDepartment,
                IdSection = employeeDTO.IdSection,
                IdDesignation = employeeDTO.IdDesignation,
                HasOvertime = employeeDTO.HasOvertime,
                HasAttendenceBonus = employeeDTO.HasAttendenceBonus,
                IdWeekOff = employeeDTO.IdWeekOff,
                Address = employeeDTO.Address,
                PresentAddress = employeeDTO.PresentAddress,
                NationalIdentificationNumber = employeeDTO.NationalIdentificationNumber,
                ContactNo = employeeDTO.ContactNo,
                IdMaritalStatus = employeeDTO.IdMaritalStatus,
                IsActive = employeeDTO.IsActive,
                SetDate = employeeDTO.SetDate,
                CreatedBy = employeeDTO.CreatedBy

            };
            return _employeeRepository.AddEmployee(employee);
        }

        public bool UpdateEmployee(EmployeeDTO employeeDTO)
        {
            var employee = new Employee
            {
                Id = employeeDTO.Id,
                IdClient = employeeDTO.IdClient,
                EmployeeName = employeeDTO.EmployeeName,
                EmployeeNameBangla = employeeDTO.EmployeeNameBangla,
                FatherName = employeeDTO.FatherName,
                MotherName = employeeDTO.MotherName,
                IdReportingManager = employeeDTO.IdReportingManager,
                IdJobType = employeeDTO.IdJobType,
                IdEmployeeType = employeeDTO.IdEmployeeType,
                BirthDate = employeeDTO.BirthDate,
                JoiningDate = employeeDTO.JoiningDate,
                IdGender = employeeDTO.IdGender,
                IdReligion = employeeDTO.IdReligion,
                IdDepartment = employeeDTO.IdDepartment,
                IdSection = employeeDTO.IdSection,
                IdDesignation = employeeDTO.IdDesignation,
                HasOvertime = employeeDTO.HasOvertime,
                HasAttendenceBonus = employeeDTO.HasAttendenceBonus,
                IdWeekOff = employeeDTO.IdWeekOff,
                Address = employeeDTO.Address,
                PresentAddress = employeeDTO.PresentAddress,
                NationalIdentificationNumber = employeeDTO.NationalIdentificationNumber,
                ContactNo = employeeDTO.ContactNo,
                IdMaritalStatus = employeeDTO.IdMaritalStatus,
                IsActive = employeeDTO.IsActive,
                SetDate = employeeDTO.SetDate,
                CreatedBy = employeeDTO.CreatedBy
            };

            return _employeeRepository.UpdateEmployee(employee);
        }

        public bool DeleteEmployee(int id)
        {
            if (id <= 0)
            {
                return false; 
            }

            return _employeeRepository.DeleteEmployee(id);
        }

      
    }
}
