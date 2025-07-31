using HRMApiApp.BLL.Interfaces;
using HRMApiApp.DAL.Interfaces;
using HRMApiApp.DTO;
using HRMApiApp.Models;
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
    public class EmployeeService(IEmployeeRepository EmployeeRepository) : IEmployeeService
    {    
        public async Task<EmployeeDTO?> GetByIdAsync(int idClient, int id, CancellationToken cancellationToken)
        {
            var employee = await EmployeeRepository.GetByIdAsync(idClient, id, cancellationToken);
            if (employee == null) return null;
            cancellationToken.ThrowIfCancellationRequested();
            var employeeDto = new EmployeeDTO
            {
                Id = employee.Id,
                IdClient = employee.IdClient,
                EmployeeName = employee.EmployeeName,
                EmployeeNameBangla = employee.EmployeeNameBangla,
                FatherName = employee.FatherName,
                MotherName = employee.MotherName,
                BirthDate = employee.BirthDate,
                IdDepartment = employee.IdDepartment,
                IdReportingManager = employee.IdReportingManager,
                IdJobType = employee.IdJobType,
                IdEmployeeType = employee.IdEmployeeType ?? null,
                IdGender = employee.IdGender ?? null,
                IdReligion = employee.IdReligion ?? null,
                IdDesignation = employee.IdDesignation ?? null,
                IdSection = employee.IdSection,
                IdWeekOff = employee.IdWeekOff ?? null,
                IdMaritalStatus = employee.IdMaritalStatus ?? null,
                DepartmentName = employee.Department.DepartName ?? "",             
                ReportingManager=employee.EmployeeName,              
                JobTypeName=employee.JobType?.JobTypeName ?? "",               
                TypeName=employee.EmployeeType?.TypeName ?? "",              
                GenderName = employee.Gender?.GenderName ?? "",               
                ReligionName=employee.Religion?.ReligionName ??"",              
                Designation = employee.Designation?.DesignationName ?? "",              
                SectionName = employee.Section.SectionName ?? "",
                JoiningDate = employee.JoiningDate,
                Address = employee.Address,
                PresentAddress = employee.PresentAddress,
                NationalIdentificationNumber = employee.NationalIdentificationNumber,
                ContactNo = employee.ContactNo,
                HasOvertime = employee.HasOvertime ?? false,
                HasAttendenceBonus = employee.HasAttendenceBonus ?? false,
                WeekOffDay =employee.WeekOff?.WeekOffDay ?? "",
                IsActive = employee.IsActive,
                MaritalStatusName=employee.MaritalStatus?.MaritalStatusName ?? "",
                CreatedBy = employee.CreatedBy ?? "",
                FileBase64 = employee.EmployeeImage != null ? Convert.ToBase64String(employee.EmployeeImage) : null,



                Documents = employee.EmployeeDocuments
                .Select(d => new EmployeeDocumentDTO
                {
                    Id = d.Id,
                    IdClient = d.IdClient,
                    DocumentName = d.DocumentName,
                    FileName = d.FileName,
                    UploadDate = d.UploadDate,
                    UploadedFileExtention = d.UploadedFileExtention,
                    FileBase64 = d.UploadedFile != null ? Convert.ToBase64String(d.UploadedFile) : null,
                   
                }).ToList(),

                EducationInfos = employee.EmployeeEducationInfos
                .Select(e => new EmployeeEducationInfoDTO
                {
                    Id = e.Id,
                    IdClient = e.IdClient,
                    //IdEducationLevel = e.IdEducationLevel,
                    EducationLevelName = e.EducationLevel?.EducationLevelName ?? "",
                    //IdEducationExamination = e.IdEducationExamination,
                    ExamName = e.EducationExamination?.ExamName ?? "",
                   // IdEducationResult = e.IdEducationResult,
                    ResultName = e.EducationResult?.ResultName ?? "",
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


                FamilyInfos = employee.EmployeeFamilyInfos
                .Select(e => new EmployeeFamilyInfoDTO
                {
                    Id = e.Id,
                    IdClient = e.IdClient,
                    //IdGender=e.IdGender,
                    //IdRelationship= e.IdRelationship,
                    Name = e.Name,
                    GenderName = e.Gender?.GenderName ?? "",
                    RelationName = e.Relationship?.RelationName ?? "",
                    ContactNo = e.ContactNo,
                    DateOfBirth = e.DateOfBirth,
                    CurrentAddress = e.CurrentAddress,
                    PermanentAddress = e.PermanentAddress,
                }).ToList(),

                Certifications = employee.EmployeeProfessionalCertifications
                .Select(c => new EmployeeProfessionalCertificationDTO
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
            return employeeDto;
        }
       
        public async Task<List<EmployeeDTO>> GetAllAsync(int idClient, CancellationToken cancellationToken)
        {
            var employees = await EmployeeRepository.GetAllAsync(idClient,cancellationToken);
         

            var allEmp= employees
                .Where(e => e.IdClient == idClient && e.IsActive==true)
                .Select(e => new EmployeeDTO
            {
                    Id = e.Id,
                    IdClient = e.IdClient,
                    EmployeeName = e.EmployeeName,
                    EmployeeNameBangla = e.EmployeeNameBangla,
                    FatherName = e.FatherName,
                    MotherName = e.MotherName,
                    BirthDate = e.BirthDate,
                    IdDepartment = e.IdDepartment,
                    DepartmentName = e.Department.DepartName ?? "",
                    IdReportingManager = e.IdReportingManager,
                    ReportingManager = e.EmployeeName,
                    IdJobType = e.IdJobType,
                    JobTypeName = e.JobType?.JobTypeName ?? "",
                    IdEmployeeType = e.IdEmployeeType ?? null,
                    TypeName = e.EmployeeType?.TypeName ?? "",
                    IdGender = e.IdGender ?? null,
                    GenderName = e.Gender?.GenderName ?? "",
                    IdReligion = e.IdReligion ?? null,
                    ReligionName = e.Religion?.ReligionName ?? "",
                    IdDesignation = e.IdDesignation ?? null,
                    Designation = e.Designation?.DesignationName ?? "",
                    IdSection = e.IdSection,
                    SectionName = e.Section.SectionName ?? "",
                    JoiningDate = e.JoiningDate,
                    Address = e.Address,
                    PresentAddress = e.PresentAddress,
                    NationalIdentificationNumber = e.NationalIdentificationNumber,
                    ContactNo = e.ContactNo,
                    HasOvertime = e.HasOvertime ?? false,
                    HasAttendenceBonus = e.HasAttendenceBonus ?? false,
                    IdWeekOff = e.IdWeekOff ?? null,
                    WeekOffDay = e.WeekOff?.WeekOffDay ?? "",
                    IsActive = e.IsActive,
                    IdMaritalStatus = e.IdMaritalStatus ?? null,
                    MaritalStatusName = e.MaritalStatus?.MaritalStatusName ?? "",
                    CreatedBy = e.CreatedBy ?? "",
                    FileBase64 = e.EmployeeImage != null ? Convert.ToBase64String(e.EmployeeImage) : null,
                //FileBase64 = e.EmployeeImage != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(e.EmployeeImage)}" : null,
                Documents = e.EmployeeDocuments
                .Select(d => new EmployeeDocumentDTO
                {
                    Id=d.Id,
                    IdClient = d.IdClient,
                    DocumentName = d.DocumentName,
                    FileName = d.FileName,
                    UploadDate = d.UploadDate,
                    UploadedFileExtention = d.UploadedFileExtention,
                    FileBase64 = d.UploadedFile != null ? Convert.ToBase64String(d.UploadedFile) : null,
                    //FileBase64 = d.UploadedFile != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(d.UploadedFile)}" : null,
                    SetDate = d.SetDate ?? null,
                    CreatedBy = d.CreatedBy,

                }).ToList(),

                EducationInfos = e.EmployeeEducationInfos
                .Select(ed => new EmployeeEducationInfoDTO
                {
                    Id=ed.Id,
                    IdClient = ed.IdClient,
                    //IdEducationLevel = ed.IdEducationLevel,
                    EducationLevelName = ed.EducationLevel?.EducationLevelName ?? "",
                    //IdEducationExamination = ed.IdEducationExamination,
                    ExamName = ed.EducationExamination?.ExamName ?? "",
                    //IdEducationResult = ed.IdEducationResult,
                    ResultName = ed.EducationResult?.ResultName ?? "",
                    Cgpa = ed.Cgpa,
                    ExamScale = ed.ExamScale,
                    Marks = ed.Marks,
                    Major = ed.Major,
                    PassingYear = ed.PassingYear,
                    InstituteName = ed.InstituteName,
                    IsForeignInstitute = ed.IsForeignInstitute,
                    Duration = ed.Duration,
                    Achievement = ed.Achievement,
                    SetDate = ed.SetDate ?? null,
                    CreatedBy = ed.CreatedBy,

                }).ToList(),

                FamilyInfos = e.EmployeeFamilyInfos
                .Select(e => new EmployeeFamilyInfoDTO
                {
                    Id = e.Id,
                    IdClient = e.IdClient,
                    //IdGender = e.IdGender,
                    //IdRelationship = e.IdRelationship,
                    GenderName = e.Gender?.GenderName ?? "",
                    RelationName = e.Relationship?.RelationName ?? "",
                    Name = e.Name,
                    ContactNo = e.ContactNo,
                    DateOfBirth = e.DateOfBirth,
                    CurrentAddress = e.CurrentAddress,
                    PermanentAddress = e.PermanentAddress,
                    SetDate = e.SetDate ?? null,
                    CreatedBy = e.CreatedBy,
                }).ToList(),

                 Certifications = e.EmployeeProfessionalCertifications
                .Select(c => new EmployeeProfessionalCertificationDTO
                {   Id = c.Id,
                    IdClient = c.IdClient,
                    CertificationTitle = c.CertificationTitle,
                    CertificationInstitute = c.CertificationInstitute,
                    InstituteLocation = c.InstituteLocation,
                    FromDate = c.FromDate,
                    ToDate = c.ToDate,
                    SetDate = c.SetDate ?? null,
                    CreatedBy = c.CreatedBy ?? null,
                }).ToList()
           
            }).ToList();
            return allEmp;

        }
        private async Task<byte[]?> ConvertFileToByteArrayAsync(IFormFile? file, CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
                return null;

            const long maxFileSize = 10 * 1024 * 1024;

            if (file.Length > maxFileSize)
                throw new Exception("File size cannot exceed 10 MB.");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream, cancellationToken);
            return memoryStream.ToArray();
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
                EmployeeImage = await ConvertFileToByteArrayAsync(employeeDto.ProfileImage,cancellationToken),
                IdDepartment = employeeDto.IdDepartment,
                IdReportingManager = employeeDto.Id,
                IdJobType=employeeDto.IdJobType,
                IdEmployeeType=employeeDto.IdEmployeeType,
                IdGender=employeeDto.IdGender,
                IdReligion=employeeDto.IdReligion,
                IdDesignation=employeeDto.IdDesignation,
                IdWeekOff=employeeDto.IdWeekOff,
                IdMaritalStatus=employeeDto.IdMaritalStatus,
                IdSection = employeeDto.IdSection,
                BirthDate = employeeDto.BirthDate,
                JoiningDate = employeeDto.JoiningDate,
                Address = employeeDto.Address,
                PresentAddress = employeeDto.PresentAddress,
                NationalIdentificationNumber = employeeDto.NationalIdentificationNumber,
                ContactNo = employeeDto.ContactNo,
                IsActive = true,
                HasOvertime = employeeDto.HasOvertime ?? false,
                HasAttendenceBonus = employeeDto.HasAttendenceBonus ?? false,
                SetDate = DateTime.Now,
                CreatedBy=employeeDto.CreatedBy,
                //EmployeeDocuments = (await Task.WhenAll(
                //employeeDto.Documents.Select(async doc =>
                //{
                //    var uploadedBytes = await ConvertFileToByteArrayAsync(doc.UpFile);
                //    var extension = Path.GetExtension(doc.UpFile?.FileName);

                //    return new EmployeeDocument
                //    {
                //        IdClient = doc.IdClient,
                //        DocumentName = doc.DocumentName,
                //        FileName = doc.FileName,
                //        UploadDate = doc.UploadDate,
                //        UploadedFileExtention = extension,
                //        UploadedFile = uploadedBytes,
                //        SetDate = DateTime.Now
                //    };
                //})
                //)).ToList(),
                EmployeeDocuments =new List<EmployeeDocument>(),


                EmployeeEducationInfos = employeeDto.EducationInfos
                .Select(e => new EmployeeEducationInfo
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

                EmployeeFamilyInfos = employeeDto.FamilyInfos
                .Select(e => new EmployeeFamilyInfo
                {
                    IdClient = e.IdClient,
                    IdGender = e.IdGender,
                    IdRelationship = e.IdRelationship,
                    Name = e.Name,
                    ContactNo = e.ContactNo,
                    DateOfBirth = e.DateOfBirth,
                    CurrentAddress = e.CurrentAddress,
                    PermanentAddress = e.PermanentAddress,
                    SetDate = DateTime.Now
                }).ToList(),

                EmployeeProfessionalCertifications = employeeDto.Certifications
                .Select(c => new EmployeeProfessionalCertification
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
                var uploadedBytes = await ConvertFileToByteArrayAsync(doc.UpFile, cancellationToken);
                var extension = Path.GetExtension(doc.UpFile?.FileName);
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

            await EmployeeRepository.CreateAsync(employee, cancellationToken);
            return true;
        }


        public async Task<string> UpdateAsync(EmployeeUpdateDTO employeeDto, CancellationToken cancellationToken)
        {
            try
            {
                await EmployeeRepository.UpdateAsync(employeeDto,cancellationToken);
                return "Success";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }

        }

        public async Task<bool> DeleteAsync(int idClient, int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return false;
            }

            await EmployeeRepository.SoftDeleteAsync(idClient, id,cancellationToken);
            return true;
        }

        //public async Task<(byte[]? fileData, string mimeType)> GetEmployeeFileAsync(int idClient, int id, string fileType, int? documentId)
        //{
        //    var employee = await _employeeRepository.GetByIdAsync(idClient, id);

        //    if (employee == null)
        //        return (null, string.Empty);

        //    byte[]? fileBytes = null;

        //    if (fileType.ToLower() == "image")
        //    {
        //        if (employee.EmployeeImage == null)
        //            return (null, string.Empty);

        //        fileBytes = employee.EmployeeImage;
        //    }
        //    else if (fileType.ToLower() == "document")
        //    {
        //        if (documentId == null)
        //            return (null, string.Empty);

        //        var document = employee.EmployeeDocuments
        //            .FirstOrDefault(d => d.IdClient == idClient && d.Id == documentId);

        //        if (document == null || document.UploadedFile == null)
        //            return (null, string.Empty);

        //        fileBytes = document.UploadedFile;
        //    }
        //    else
        //    {
        //        return (null, string.Empty);
        //    }

        //    var mimeType = GetMimeTypeFromBytes(fileBytes);
        //    return (fileBytes, mimeType);
        //}

        //private string GetMimeTypeFromBytes(byte[] data)
        //{
        //    if (data.Length > 4)
        //    {
        //        if (data[0] == 0xFF && data[1] == 0xD8)
        //            return "image/jpeg";
        //        if (data[0] == 0x89 && data[1] == 0x50 && data[2] == 0x4E && data[3] == 0x47)
        //            return "image/png";
        //        if (data[0] == 0x47 && data[1] == 0x49 && data[2] == 0x46)
        //            return "image/gif";
        //        if (data[0] == 0x25 && data[1] == 0x50 && data[2] == 0x44 && data[3] == 0x46)
        //            return "application/pdf";
        //        if (data[0] == 0x50 && data[1] == 0x4B && data[2] == 0x03 && data[3] == 0x04)
        //            return "application/zip";
        //        if (data[0] == 0xD0 && data[1] == 0xCF && data[2] == 0x11 && data[3] == 0xE0)
        //            return "application/vnd.ms-excel";

        //        bool isText = true;
        //        for (int i = 0; i < Math.Min(data.Length, 512); i++)
        //        {
        //            if (data[i] < 0x09) { isText = false; break; }
        //            if (data[i] > 0x0D && data[i] < 0x20) { isText = false; break; }
        //        }
        //        if (isText)
        //            return "text/plain";
        //    }

        //    return "application/octet-stream";
        //}

    }
}
