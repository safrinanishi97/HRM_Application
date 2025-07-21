using HRMApiApp.BLL.Interfaces;
using HRMApiApp.DAL.Interfaces;
using HRMApiApp.DTO;
using HRMApiApp.Models;
using HRMApiApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.ConstrainedExecution;
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

        public async Task<EmployeeDTO?> GetByIdAsync(int id, int idClient, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(id,idClient, cancellationToken);
            if (employee == null) return null;

            var emloyee = new EmployeeDTO
            {
                Id = employee.Id,
                EmployeeName = employee.EmployeeName,
                EmployeeNameBangla = employee.EmployeeNameBangla,
                FatherName = employee.FatherName,
                MotherName = employee.MotherName,
                BirthDate = employee.BirthDate,
                IdDepartment = employee.Id,
                IdSection= employee.IdSection,
                JoiningDate = employee.JoiningDate,
                Address = employee.Address,
                PresentAddress = employee.PresentAddress,
                NationalIdentificationNumber = employee.NationalIdentificationNumber,
                ContactNo = employee.ContactNo,
                IsActive = employee.IsActive,

                Documents = employee.EmployeeDocuments.Select(d => new EmployeeDocumentDTO
                {
                    Id = d.Id,
                    IdClient = d.IdClient,
                    DocumentName = d.DocumentName,
                    FileName = d.FileName,
                    UploadDate = d.UploadDate,
                    UploadedFileExtention = d.UploadedFileExtention,
                    //UploadedFile = d.UploadedFile
                }).ToList(),

                EducationInfos = employee.EmployeeEducationInfos.Select(e => new EmployeeEducationInfoDTO
                {
                    Id = e.Id,
                    IdClient = e.IdClient,
                    IdEducationLevel = e.IdEducationLevel,
                    IdEducationExamination = e.IdEducationExamination,
                    IdEducationResult = e.IdEducationResult,
                    Cgpa = e.Cgpa,
                    ExamScale = e.ExamScale,
                    Marks = e.Marks,
                    Major = e.Major,
                    PassingYear = e.PassingYear,
                    InstituteName = e.InstituteName,
                    IsForeignInstitute = e.IsForeignInstitute,
                    Duration = e.Duration,
                    Achievement = e.Achievement
                }).ToList(),

                Certifications = employee.EmployeeProfessionalCertifications.Select(c => new EmployeeProfessionalCertificationDTO
                {
                    Id = c.Id,
                    IdClient = c.IdClient,
                    CertificationTitle = c.CertificationTitle,
                    CertificationInstitute = c.CertificationInstitute,
                    InstituteLocation = c.InstituteLocation,
                    FromDate = c.FromDate,
                    ToDate = c.ToDate
                }).ToList()
            };
            return emloyee;
        }

        public async Task<List<EmployeeDTO>> GetAllAsync(CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetAllAsync(cancellationToken);

            var allEmp= employees.Select(e => new EmployeeDTO
            {
                Id = e.Id,
                IdClient = e.IdClient,
                EmployeeName = e.EmployeeName,
                EmployeeNameBangla = e.EmployeeNameBangla,
                FatherName = e.FatherName,
                MotherName = e.MotherName,
                BirthDate = e.BirthDate,
                IdDepartment=e.IdDepartment,
                IdSection=e.IdSection,
                JoiningDate = e.JoiningDate,
                Address = e.Address,
                PresentAddress = e.PresentAddress,
                NationalIdentificationNumber = e.NationalIdentificationNumber,
                ContactNo = e.ContactNo,
                IsActive = e.IsActive,
                Documents = e.EmployeeDocuments.Select(d => new EmployeeDocumentDTO
                {
                    IdClient = d.IdClient,
                    DocumentName = d.DocumentName,
                    FileName = d.FileName,
                    UploadDate = d.UploadDate,
                    UploadedFileExtention = d.UploadedFileExtention,
                   // UploadedFile = d.UploadedFile
                }).ToList(),

                EducationInfos = e.EmployeeEducationInfos.Select(ed => new EmployeeEducationInfoDTO
                {
                    IdClient = ed.IdClient,
                    IdEducationLevel = ed.IdEducationLevel,
                    IdEducationExamination = ed.IdEducationExamination,
                    IdEducationResult = ed.IdEducationResult,
                    Cgpa = ed.Cgpa,
                    ExamScale = ed.ExamScale,
                    Marks = ed.Marks,
                    Major = ed.Major,
                    PassingYear = ed.PassingYear,
                    InstituteName = ed.InstituteName,
                    IsForeignInstitute = ed.IsForeignInstitute,
                    Duration = ed.Duration,
                    Achievement = ed.Achievement
                }).ToList(),

                Certifications = e.EmployeeProfessionalCertifications.Select(c => new EmployeeProfessionalCertificationDTO
                {
                    IdClient = c.IdClient,
                    CertificationTitle = c.CertificationTitle,
                    CertificationInstitute = c.CertificationInstitute,
                    InstituteLocation = c.InstituteLocation,
                    FromDate = c.FromDate,
                    ToDate = c.ToDate
                }).ToList()
           
            }).ToList();
            return allEmp;
           
        }

        public async Task<bool> CreateAsync(EmployeeCreateDTO employeeDto, CancellationToken cancellationToken)
        {
            var employee = new Employee
            {
                IdClient = employeeDto.IdClient,
                EmployeeName = employeeDto.EmployeeName,
                EmployeeNameBangla = employeeDto.EmployeeNameBangla,
                FatherName = employeeDto.FatherName,
                MotherName = employeeDto.MotherName,
                IdDepartment = employeeDto.IdDepartment,
                IdSection = employeeDto.IdSection,
                BirthDate = employeeDto.BirthDate,
                JoiningDate = employeeDto.JoiningDate,
                Address = employeeDto.Address,
                PresentAddress = employeeDto.PresentAddress,
                NationalIdentificationNumber = employeeDto.NationalIdentificationNumber,
                ContactNo = employeeDto.ContactNo,
                IsActive = true,
                SetDate = DateTime.UtcNow,

                EmployeeDocuments = employeeDto.Documents.Select(d => new EmployeeDocument
                {
                    IdClient = d.IdClient,
                    DocumentName = d.DocumentName,
                    FileName = d.FileName,
                    UploadDate = d.UploadDate,
                    UploadedFileExtention = d.UploadedFileExtention,
                    //UploadedFile = d.UploadedFile,
                    SetDate = DateTime.UtcNow
                }).ToList(),

                EmployeeEducationInfos = employeeDto.EducationInfos.Select(e => new EmployeeEducationInfo
                {
                    IdClient = e.IdClient,
                    IdEducationLevel = e.IdEducationLevel,
                    IdEducationExamination = e.IdEducationExamination,
                    IdEducationResult = e.IdEducationResult,
                    Cgpa = e.Cgpa,
                    ExamScale = e.ExamScale,
                    Marks = e.Marks,
                    Major = e.Major,
                    PassingYear = e.PassingYear,
                    InstituteName = e.InstituteName,
                    IsForeignInstitute = e.IsForeignInstitute,
                    Duration = e.Duration,
                    Achievement = e.Achievement,
                    SetDate = DateTime.UtcNow
                }).ToList(),

                EmployeeProfessionalCertifications = employeeDto.Certifications.Select(c => new EmployeeProfessionalCertification
                {
                    IdClient = c.IdClient,
                    CertificationTitle = c.CertificationTitle,
                    CertificationInstitute = c.CertificationInstitute,
                    InstituteLocation = c.InstituteLocation,
                    FromDate = c.FromDate,
                    ToDate = c.ToDate,
                    SetDate = DateTime.UtcNow
                }).ToList()
            };

            var createdEmployee = await _employeeRepository.CreateAsync(employee, cancellationToken);
            return true;
        }

        public async Task<string> UpdateAsync(EmployeeUpdateDTO employeeDto, CancellationToken cancellationToken)
        {
            var existingEmployee = await _employeeRepository.GetByIdForUpdate(employeeDto.IdClient, employeeDto.Id, cancellationToken);
            if (existingEmployee == null)
                return "Employee not found";

            existingEmployee.EmployeeName = employeeDto.EmployeeName ?? existingEmployee.EmployeeName;
            existingEmployee.EmployeeNameBangla = employeeDto.EmployeeNameBangla ?? existingEmployee.EmployeeNameBangla;
            existingEmployee.FatherName = employeeDto.FatherName ?? existingEmployee.FatherName;
            existingEmployee.MotherName = employeeDto.MotherName ?? existingEmployee.MotherName;
            existingEmployee.IdDepartment = employeeDto.IdDepartment;
            existingEmployee.IdSection = employeeDto.IdSection;
            existingEmployee.BirthDate = employeeDto.BirthDate ?? existingEmployee.BirthDate;
            existingEmployee.Address = employeeDto.Address ?? existingEmployee.Address;
            existingEmployee.PresentAddress = employeeDto.PresentAddress ?? existingEmployee.PresentAddress;
            existingEmployee.NationalIdentificationNumber = employeeDto.NationalIdentificationNumber ?? existingEmployee.NationalIdentificationNumber;
            existingEmployee.ContactNo = employeeDto.ContactNo ?? existingEmployee.ContactNo;
            existingEmployee.IsActive = employeeDto.IsActive ?? existingEmployee.IsActive;
            existingEmployee.SetDate = DateTime.UtcNow;


            existingEmployee.EmployeeDocuments = employeeDto.Documents.Select(docDto => new EmployeeDocument
            {
                Id = docDto.Id,
                IdClient = docDto.IdClient,
                IdEmployee = docDto.IdEmployee,
                DocumentName = docDto.DocumentName,
                FileName = docDto.FileName,
                UploadDate = docDto.UploadDate,
                UploadedFileExtention = docDto.UploadedFileExtention,
                SetDate = DateTime.UtcNow
            }).ToList();

            existingEmployee.EmployeeEducationInfos = employeeDto.EducationInfos.Select(eduDto => new EmployeeEducationInfo
            {
                Id = eduDto.Id,
                IdClient = eduDto.IdClient,
                IdEmployee = eduDto.IdEmployee,
                IdEducationLevel = eduDto.IdEducationLevel,
                IdEducationExamination = eduDto.IdEducationExamination,
                IdEducationResult = eduDto.IdEducationResult,
                Cgpa = eduDto.Cgpa,
                Marks = eduDto.Marks,
                PassingYear = eduDto.PassingYear,
                InstituteName = eduDto.InstituteName,
                Major = eduDto.Major,
                IsForeignInstitute = eduDto.IsForeignInstitute,
                Duration = eduDto.Duration,
                Achievement = eduDto.Achievement,
                SetDate = DateTime.UtcNow
            }).ToList();

            existingEmployee.EmployeeProfessionalCertifications = employeeDto.Certifications.Select(certDto => new EmployeeProfessionalCertification
            {
                Id = certDto.Id,
                IdClient = certDto.IdClient,
                IdEmployee = certDto.IdEmployee,
                CertificationTitle = certDto.CertificationTitle,
                CertificationInstitute = certDto.CertificationInstitute,
                InstituteLocation = certDto.InstituteLocation,
                FromDate = certDto.FromDate,
                ToDate = certDto.ToDate,
                SetDate = DateTime.UtcNow
            }).ToList();


            try
            {
                await _employeeRepository.UpdateAsync(existingEmployee, cancellationToken);
                return "Success";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }

        }





        //public async Task<string> UpdateAsync(EmployeeUpdateDTO employeeDto, CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //        var existingEmployee = await _employeeRepository.GetByIdForUpdate(employeeDto.IdClient, employeeDto.Id, cancellationToken);
        //        if (existingEmployee == null)
        //            return "Employee not found";

        //        existingEmployee.EmployeeName = employeeDto.EmployeeName ?? existingEmployee.EmployeeName;
        //        existingEmployee.EmployeeNameBangla = employeeDto.EmployeeNameBangla ?? existingEmployee.EmployeeNameBangla;
        //        existingEmployee.FatherName = employeeDto.FatherName ?? existingEmployee.FatherName;
        //        existingEmployee.MotherName = employeeDto.MotherName ?? existingEmployee.MotherName;
        //        existingEmployee.BirthDate = employeeDto.BirthDate ?? existingEmployee.BirthDate;
              
        //        existingEmployee.Address = employeeDto.Address ?? existingEmployee.Address;
        //        existingEmployee.PresentAddress = employeeDto.PresentAddress ?? existingEmployee.PresentAddress;
        //        existingEmployee.NationalIdentificationNumber = employeeDto.NationalIdentificationNumber ?? existingEmployee.NationalIdentificationNumber;
        //        existingEmployee.ContactNo = employeeDto.ContactNo ?? existingEmployee.ContactNo;
        //        existingEmployee.IdDepartment = employeeDto.IdDepartment;
        //        existingEmployee.IdSection = employeeDto.IdSection;
        //        existingEmployee.IsActive = employeeDto.IsActive ?? existingEmployee.IsActive;
        //        existingEmployee.SetDate = DateTime.UtcNow;

        //        if (employeeDto.Documents != null)
        //        {
        //            existingEmployee.EmployeeDocuments.Clear();
        //            foreach (var doc in employeeDto.Documents)
        //            {
        //                existingEmployee.EmployeeDocuments.Add(new EmployeeDocument
        //                {
        //                    IdClient = doc.IdClient,
        //                    IdEmployee=doc.IdEmployee,
        //                    DocumentName = doc.DocumentName,
        //                    FileName = doc.FileName,
        //                    UploadDate = doc.UploadDate,
        //                    UploadedFileExtention = doc.UploadedFileExtention,
        //                    SetDate = DateTime.UtcNow
        //                });
        //            }
        //        }

        //        if (employeeDto.EducationInfos != null)
        //        {
        //            existingEmployee.EmployeeEducationInfos.Clear();
        //            foreach (var edu in employeeDto.EducationInfos)
        //            {
        //                existingEmployee.EmployeeEducationInfos.Add(new EmployeeEducationInfo
        //                {
        //                    IdClient = edu.IdClient,
        //                    IdEmployee = edu.IdEmployee,
        //                    IdEducationLevel = edu.IdEducationLevel,
        //                    IdEducationExamination = edu.IdEducationExamination,
        //                    IdEducationResult = edu.IdEducationResult,
        //                    Cgpa = edu.Cgpa,
        //                    Marks = edu.Marks,
        //                    Major = edu.Major,
        //                    PassingYear = edu.PassingYear,
        //                    InstituteName = edu.InstituteName,
        //                    IsForeignInstitute = edu.IsForeignInstitute,
        //                    Duration = edu.Duration,
        //                    Achievement = edu.Achievement,
        //                    SetDate = DateTime.UtcNow
        //                });
        //            }
        //        }

             
        //        if (employeeDto.Certifications != null)
        //        {
        //            existingEmployee.EmployeeProfessionalCertifications.Clear();
        //            foreach (var cert in employeeDto.Certifications)
        //            {
        //                existingEmployee.EmployeeProfessionalCertifications.Add(new EmployeeProfessionalCertification
        //                {
        //                    IdClient = cert.IdClient,
        //                    IdEmployee = cert.IdEmployee,
        //                    CertificationTitle = cert.CertificationTitle,
        //                    CertificationInstitute = cert.CertificationInstitute,
        //                    InstituteLocation = cert.InstituteLocation,
        //                    FromDate = cert.FromDate,
        //                    ToDate = cert.ToDate,
        //                    SetDate = DateTime.UtcNow
        //                });
        //            }
        //        }

        //        await _employeeRepository.UpdateAsync(existingEmployee, cancellationToken);
        //        return "Success";
        //    }
        //    catch (Exception ex)
        //    {
        //        return $"Error: {ex.Message}";
        //    }
        //}






        public async Task<bool> DeleteAsync(int idClient, int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return false;
            }

            await _employeeRepository.SoftDeleteAsync(idClient, id,cancellationToken);
            return true;
        }
     
    }
}
