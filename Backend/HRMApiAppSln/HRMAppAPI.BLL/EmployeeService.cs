using HRMApiApp.BLL.Interfaces;
using HRMApiApp.DAL.Interfaces;
using HRMApiApp.DTO;
using HRMApiApp.Models;
using HRMApiApp.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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
        private readonly IHostEnvironment _environment;

        public EmployeeService(IEmployeeRepository employeeRepository, IHostEnvironment environment)
        {
            _employeeRepository = employeeRepository;
            _environment = environment;
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


        //private async Task<string?> SaveFileAsync(IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //        return null;

        //    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

        //    var uploadPath = Path.Combine(_environment.ContentRootPath, "uploads/employees");
        //    Directory.CreateDirectory(uploadPath);

        //    var filePath = Path.Combine(uploadPath, fileName);

        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }

        //    return $"/uploads/employees/{fileName}";
        //}



        public async Task<bool> CreateAsync(EmployeeCreateDTO employeeDto, CancellationToken cancellationToken)
        {

            async Task<byte[]?> ConvertFileToByteArrayAsync(IFormFile? file)
            {
                if (file == null || file.Length == 0)
                    return null;

                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }

            var employee = new Employee
            {
                IdClient = employeeDto.IdClient,
                EmployeeName = employeeDto.EmployeeName,
                EmployeeNameBangla = employeeDto.EmployeeNameBangla,
                FatherName = employeeDto.FatherName,
                MotherName = employeeDto.MotherName,
                EmployeeImage = await ConvertFileToByteArrayAsync(employeeDto.ProfileImage),
                IdDepartment = employeeDto.IdDepartment,
                IdSection = employeeDto.IdSection,
                BirthDate = employeeDto.BirthDate,
                JoiningDate = employeeDto.JoiningDate,
                Address = employeeDto.Address,
                PresentAddress = employeeDto.PresentAddress,
                NationalIdentificationNumber = employeeDto.NationalIdentificationNumber,
                ContactNo = employeeDto.ContactNo,
                IsActive = true,
                SetDate = DateTime.Now,

                EmployeeDocuments = new List<EmployeeDocument>(),
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
                    SetDate = DateTime.Now
                }).ToList(),

                EmployeeProfessionalCertifications = employeeDto.Certifications.Select(c => new EmployeeProfessionalCertification
                {
                    IdClient = c.IdClient,
                    CertificationTitle = c.CertificationTitle,
                    CertificationInstitute = c.CertificationInstitute,
                    InstituteLocation = c.InstituteLocation,
                    FromDate = c.FromDate,
                    ToDate = c.ToDate,
                    SetDate = DateTime.Now
                }).ToList()
            };

   
            foreach (var doc in employeeDto.Documents)
            {
                var uploadedBytes = await ConvertFileToByteArrayAsync(doc.File);
                var extension = Path.GetExtension(doc.File?.FileName);
                employee.EmployeeDocuments.Add(new EmployeeDocument
                {
                    IdClient = doc.IdClient,
                    DocumentName = doc.DocumentName,
                    FileName = doc.FileName,
                    UploadDate = doc.UploadDate,
                    UploadedFileExtention = extension,
                    UploadedFile = uploadedBytes,
                    SetDate = DateTime.Now
                });
            }

            await _employeeRepository.CreateAsync(employee, cancellationToken);
            return true;
        }


        public async Task<string> UpdateAsync(EmployeeUpdateDTO employeeDto, CancellationToken cancellationToken)
        {
            try
            {
                await _employeeRepository.UpdateAsync(employeeDto, cancellationToken);
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
