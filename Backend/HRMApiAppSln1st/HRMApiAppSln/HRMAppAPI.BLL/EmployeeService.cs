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
        public Task<Employee?> GetEmployeeById(int idClient, int id)
        {
            return _employeeRepository.GetEmployeeById(idClient, id);
        }
        //public bool AddEmployee(EmployeeDTO employeeDTO)
        //{
        //    var employee = new Employee
        //    {
        //        IdClient = employeeDTO.IdClient,
        //        EmployeeName = employeeDTO.EmployeeName,
        //        EmployeeNameBangla = employeeDTO.EmployeeNameBangla,
        //        //EmployeeImage = employeeDTO.EmployeeImage,
        //        FatherName = employeeDTO.FatherName,
        //        MotherName = employeeDTO.MotherName,
        //        IdReportingManager = employeeDTO.IdReportingManager,
        //        IdJobType = employeeDTO.IdJobType,
        //        IdEmployeeType = employeeDTO.IdEmployeeType,
        //        BirthDate = employeeDTO.BirthDate,
        //        JoiningDate = employeeDTO.JoiningDate,
        //        IdGender = employeeDTO.IdGender,
        //        IdReligion = employeeDTO.IdReligion,
        //        IdDepartment = employeeDTO.IdDepartment,
        //        IdSection = employeeDTO.IdSection,
        //        IdDesignation = employeeDTO.IdDesignation,
        //        HasOvertime = employeeDTO.HasOvertime,
        //        HasAttendenceBonus = employeeDTO.HasAttendenceBonus,
        //        IdWeekOff = employeeDTO.IdWeekOff,
        //        Address = employeeDTO.Address,
        //        PresentAddress = employeeDTO.PresentAddress,
        //        NationalIdentificationNumber = employeeDTO.NationalIdentificationNumber,
        //        ContactNo = employeeDTO.ContactNo,
        //        IdMaritalStatus = employeeDTO.IdMaritalStatus,
        //        IsActive = employeeDTO.IsActive,
        //        SetDate = DateTime.Now,
        //        CreatedBy = employeeDTO.CreatedBy

        //    };
        //    return _employeeRepository.AddEmployee(employee);
        //}


        public async Task<Employee?> CreateEmployeeWithDetails(EmployeeDetailDTO employeeDetail)
        {
            try
            {
                var employee = new Employee
                {
                    IdClient = employeeDetail.IdClient,
                    EmployeeName = employeeDetail.EmployeeName,
                    EmployeeNameBangla = employeeDetail.EmployeeNameBangla,
                    //EmployeeImage = employeeDTO.EmployeeImage,
                    FatherName = employeeDetail.FatherName,
                    MotherName = employeeDetail.MotherName,
                    IdReportingManager = employeeDetail.IdReportingManager,
                    IdJobType = employeeDetail.IdJobType,
                    IdEmployeeType = employeeDetail.IdEmployeeType,
                    BirthDate = employeeDetail.BirthDate,
                    JoiningDate = employeeDetail.JoiningDate,
                    IdGender = employeeDetail.IdGender,
                    IdReligion = employeeDetail.IdReligion,
                    IdDepartment = employeeDetail.IdDepartment,
                    IdSection = employeeDetail.IdSection,
                    IdDesignation = employeeDetail.IdDesignation,
                    HasOvertime = employeeDetail.HasOvertime,
                    HasAttendenceBonus = employeeDetail.HasAttendenceBonus,
                    IdWeekOff = employeeDetail.IdWeekOff,
                    Address = employeeDetail.Address,
                    PresentAddress = employeeDetail.PresentAddress,
                    NationalIdentificationNumber = employeeDetail.NationalIdentificationNumber,
                    ContactNo = employeeDetail.ContactNo,
                    IdMaritalStatus = employeeDetail.IdMaritalStatus,
                    IsActive = employeeDetail.IsActive,
                    SetDate = DateTime.Now,
                    CreatedBy = employeeDetail.CreatedBy
                };

                employee.EmployeeDocuments = employeeDetail.Documents.Select(d => new EmployeeDocument
                {
                    IdClient = employeeDetail.IdClient,
                    DocumentName = d.DocumentName,
                    FileName = d.FileName,
                    UploadDate = d.UploadDate,
                    SetDate = DateTime.Now,
                    CreatedBy = employeeDetail.CreatedBy
                }).ToList();

                employee.EmployeeEducationInfos = employeeDetail.Educations.Select(e => new EmployeeEducationInfo
                {
                    IdClient = employeeDetail.IdClient,
                    IdEducationLevel = e.IdEducationLevel,
                    Major = e.Major,
                    SetDate = DateTime.Now,
                    CreatedBy = employeeDetail.CreatedBy
                }).ToList();

                employee.EmployeeProfessionalCertifications = employeeDetail.Certifications.Select(c => new EmployeeProfessionalCertification
                {
                    IdClient = employeeDetail.IdClient,
                    CertificationTitle = c.CertificationTitle,
                    CertificationInstitute = c.CertificationInstitute,
                    SetDate = DateTime.Now,
                    CreatedBy = employeeDetail.CreatedBy
                }).ToList();

                var result = await _employeeRepository.AddEmployee(employee);

                return result; 
            }
            catch
            {
                return null;
            }
        }


        //public bool UpdateEmployee(EmployeeDTO employeeDTO)
        //{
        //    var employee = new Employee
        //    {
        //        Id = employeeDTO.Id,
        //        IdClient = employeeDTO.IdClient,
        //        EmployeeName = employeeDTO.EmployeeName,
        //        EmployeeNameBangla = employeeDTO.EmployeeNameBangla,
        //        FatherName = employeeDTO.FatherName,
        //        MotherName = employeeDTO.MotherName,
        //        IdReportingManager = employeeDTO.IdReportingManager,
        //        IdJobType = employeeDTO.IdJobType,
        //        IdEmployeeType = employeeDTO.IdEmployeeType,
        //        BirthDate = employeeDTO.BirthDate,
        //        JoiningDate = employeeDTO.JoiningDate,
        //        IdGender = employeeDTO.IdGender,
        //        IdReligion = employeeDTO.IdReligion,
        //        IdDepartment = employeeDTO.IdDepartment,
        //        IdSection = employeeDTO.IdSection,
        //        IdDesignation = employeeDTO.IdDesignation,
        //        HasOvertime = employeeDTO.HasOvertime,
        //        HasAttendenceBonus = employeeDTO.HasAttendenceBonus,
        //        IdWeekOff = employeeDTO.IdWeekOff,
        //        Address = employeeDTO.Address,
        //        PresentAddress = employeeDTO.PresentAddress,
        //        NationalIdentificationNumber = employeeDTO.NationalIdentificationNumber,
        //        ContactNo = employeeDTO.ContactNo,
        //        IdMaritalStatus = employeeDTO.IdMaritalStatus,
        //        IsActive = employeeDTO.IsActive,
        //        SetDate = employeeDTO.SetDate,
        //        CreatedBy = employeeDTO.CreatedBy
        //    };

        //    return _employeeRepository.UpdateEmployee(employee);
        //}


        public async Task<bool> UpdateEmployee(EmployeeDTO dto)
        {
            if (dto == null || dto.Id <= 0 || dto.IdClient <= 0)
                return false;
            var employee = await _employeeRepository.GetEmployeeForUpdate(dto.IdClient, dto.Id);

            if (employee == null)
                return false;

            employee.EmployeeName = dto.EmployeeName;
            employee.EmployeeNameBangla = dto.EmployeeNameBangla;
            employee.FatherName = dto.FatherName;
            employee.MotherName = dto.MotherName;
            employee.IdReportingManager = dto.IdReportingManager;
            employee.IdEmployeeType = dto.IdEmployeeType;
            employee.BirthDate = dto.BirthDate;
            employee.JoiningDate = dto.JoiningDate;
            employee.IdGender = dto.IdGender;
            employee.IdReligion = dto.IdReligion;
            employee.IdJobType = dto.IdJobType > 0 ? dto.IdJobType : employee.IdJobType;
            employee.IdDepartment = dto.IdDepartment > 0 ? dto.IdDepartment : employee.IdDepartment;
            employee.IdSection = dto.IdSection;
            employee.IdDesignation = dto.IdDesignation;
            employee.HasOvertime = dto.HasOvertime;
            employee.HasAttendenceBonus = dto.HasAttendenceBonus;
            employee.IdWeekOff = dto.IdWeekOff;
            employee.Address = dto.Address;
            employee.PresentAddress = dto.PresentAddress;
            employee.NationalIdentificationNumber = dto.NationalIdentificationNumber;
            employee.ContactNo = dto.ContactNo;
            employee.IdMaritalStatus = dto.IdMaritalStatus;
            employee.IsActive = dto.IsActive;
       

            var result = await _employeeRepository.SaveChangesAsync();
            return result > 0;
        }


        public async Task<bool> DeleteEmployee(int idClient, int id)
        {
            if (id <= 0)
            {
                return false; 
            }

            await _employeeRepository.DeleteEmployee(idClient, id);
            return true;
        }

       
    }
}
