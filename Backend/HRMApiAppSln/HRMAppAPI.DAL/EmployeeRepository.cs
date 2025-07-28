using HRMApiApp.DAL.Interfaces;
using HRMApiApp.DTO;
using HRMApiApp.Model;
using HRMApiApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HRMApiApp.DAL
{


    public class EmployeeRepository(HanaHrmContext Context) : IEmployeeRepository
    {
        public async Task<Employee?> GetByIdAsync(int idClient, int id, CancellationToken cancellationToken)
        {
            var emp = await Context.Employees
              .AsNoTracking()
                .Include(e => e.EmployeeDocuments)
                .Include(e => e.EmployeeEducationInfos)
                .Include(e=> e.EmployeeFamilyInfos)
                .Include(e => e.EmployeeProfessionalCertifications)
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.IdClient == idClient && e.Id == id,cancellationToken);
            return emp;
        }

        public async Task<List<Employee>> GetAllAsync(int idClient, CancellationToken cancellationToken)
        {
            var emp= await Context.Employees
                .AsNoTracking()
                .Include(e => e.EmployeeDocuments)
                .Include(e => e.EmployeeEducationInfos)
                 .Include(e => e.EmployeeFamilyInfos)
                .Include(e => e.EmployeeProfessionalCertifications)
                .AsSplitQuery()
                .Where(e => e.IdClient == idClient)
                .ToListAsync(cancellationToken);
            return emp;
        }

        public async Task<bool> CreateAsync(Employee employee, CancellationToken cancellationToken)
        {
            Context.Employees.Add(employee);
            await Context.SaveChangesAsync(cancellationToken);
            return true;
        }
       private async Task<byte[]?> ConvertFileToByteArrayAsync(IFormFile? file,CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
                return null;

            const long maxFileSize = 10 * 1024 * 1024;

            if (file.Length > maxFileSize)
                throw new Exception("File size cannot exceed 10 MB.");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream,cancellationToken);
            return memoryStream.ToArray();
        }
        public async Task<int> UpdateAsync(EmployeeUpdateDTO employee, CancellationToken cancellationToken)
        {
            if (employee == null)
                throw new Exception($"data not found: {nameof(employee)}");

            var idClient = employee.IdClient;
            var id = employee.Id;

            var existingEmployee = await Context.Employees
                .Include(e => e.EmployeeDocuments)
                .Include(e => e.EmployeeEducationInfos)
                 .Include(e => e.EmployeeFamilyInfos)
                .Include(e => e.EmployeeProfessionalCertifications)
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.IdClient == idClient && e.Id == id);
            var newImage = await ConvertFileToByteArrayAsync(employee.ProfileImage, cancellationToken);
            if (existingEmployee == null) return 0;

            existingEmployee.EmployeeName = employee.EmployeeName ?? existingEmployee.EmployeeName;
            existingEmployee.EmployeeNameBangla = employee.EmployeeNameBangla ?? existingEmployee.EmployeeNameBangla;
            existingEmployee.FatherName = employee.FatherName ?? existingEmployee.FatherName;
            existingEmployee.MotherName = employee.MotherName ?? existingEmployee.MotherName;
            //existingEmployee.EmployeeImage = await ConvertFileToByteArrayAsync(employee.ProfileImage,cancellationToken) ?? existingEmployee.EmployeeImage;
            if (newImage != null)
            {
                existingEmployee.EmployeeImage = newImage;
            }

            existingEmployee.IdDepartment = employee.IdDepartment;
            existingEmployee.IdSection =   employee.IdSection;
            existingEmployee.BirthDate = employee.BirthDate ?? existingEmployee.BirthDate;
            existingEmployee.Address = employee.Address ?? existingEmployee.Address;
            existingEmployee.PresentAddress =   employee.PresentAddress ?? existingEmployee.PresentAddress;
            existingEmployee.NationalIdentificationNumber = employee.NationalIdentificationNumber ?? existingEmployee.NationalIdentificationNumber;
            existingEmployee.ContactNo = employee.ContactNo ?? existingEmployee.ContactNo;
            existingEmployee.IsActive = employee.IsActive ?? existingEmployee.IsActive;
            existingEmployee.SetDate = DateTime.Now;


            //delete obsolete data

            var deletedEmployeeDocumentList = existingEmployee.EmployeeDocuments
                .Where(ed => ed.IdClient == idClient && !employee.Documents.Any(d => d.IdClient == ed.IdClient && d.Id == ed.Id))
                .ToList();
            if(deletedEmployeeDocumentList.Any())
            {
                Context.EmployeeDocuments.RemoveRange(deletedEmployeeDocumentList);
            }

            var deletedEmployeeEducationInfoList = existingEmployee.EmployeeEducationInfos
                .Where(eei => eei.IdClient == idClient && !employee.EducationInfos.Any(ei => ei.IdClient == eei.IdClient && ei.Id == eei.Id))
                .ToList();
            if (deletedEmployeeEducationInfoList.Any())
            {
                Context.EmployeeEducationInfos.RemoveRange(deletedEmployeeEducationInfoList);
            }


            var deletedFamilyInfoList = existingEmployee.EmployeeFamilyInfos
               .Where(efi => efi.IdClient == idClient && !employee.FamilyInfos.Any(c => c.IdClient == efi.IdClient && c.Id == efi.Id))
               .ToList();

            if (deletedFamilyInfoList.Any())
            {
                Context.EmployeeFamilyInfos.RemoveRange(deletedFamilyInfoList);
            }



            var deletedCertificationList = existingEmployee.EmployeeProfessionalCertifications
                .Where(epc => epc.IdClient == idClient && !employee.Certifications.Any(c => c.IdClient == epc.IdClient && c.Id == epc.Id))
                .ToList();

            if (deletedCertificationList.Any()) 
            {
                Context.EmployeeProfessionalCertifications.RemoveRange(deletedCertificationList);
            }


            //up/insert information
            
            foreach(var item in employee.Documents)
            {
                var existingEntry = existingEmployee.EmployeeDocuments.FirstOrDefault(ed=>ed.IdClient == item.IdClient && ed.Id == item.Id);
                byte[]? uploadedBytes = null;
                string? extension = null;

                if (item.UpFile != null)
                {
                    uploadedBytes = await ConvertFileToByteArrayAsync(item.UpFile, cancellationToken);
                    extension = Path.GetExtension(item.UpFile.FileName);
                }

                if (existingEntry != null) 
                {
                    existingEntry.DocumentName = item.DocumentName;
                    existingEntry.FileName = item.FileName;
                    existingEntry.UploadDate = item.UploadDate;
                    existingEntry.UploadedFileExtention = extension ?? item.UploadedFileExtention;
                    if (uploadedBytes != null)
                    {
                        existingEntry.UploadedFile = uploadedBytes;
                    }
                    existingEntry.SetDate = DateTime.Now;
                }
                else
                {
                    var newEmployeeDocument = new EmployeeDocument()
                    {
                        IdClient = item.IdClient,
                        IdEmployee = existingEmployee.Id,
                        DocumentName = item.DocumentName,
                        FileName = item.FileName,
                        UploadDate = item.UploadDate,
                        UploadedFileExtention = extension,
                        UploadedFile = uploadedBytes,
                        SetDate = DateTime.Now
                    };

                    existingEmployee.EmployeeDocuments.Add(newEmployeeDocument);
                }
            }


            foreach (var item in employee.EducationInfos)
            {
                var existingEntry = existingEmployee.EmployeeEducationInfos.FirstOrDefault(ei => ei.IdClient == item.IdClient && ei.Id == item.Id);
                if (existingEntry != null)
                {
                    existingEntry.IdEducationLevel = item.IdEducationLevel;
                    existingEntry.IdEducationExamination = item.IdEducationExamination;
                    existingEntry.IdEducationResult = item.IdEducationResult;
                    existingEntry.Cgpa = item.Cgpa;
                    existingEntry.Marks = item.Marks;
                    existingEntry.PassingYear = item.PassingYear;
                    existingEntry.InstituteName = item.InstituteName;
                    existingEntry.Major = item.Major;
                    existingEntry.IsForeignInstitute = item.IsForeignInstitute;
                    existingEntry.Duration = item.Duration;
                    existingEntry.Achievement = item.Achievement;
                    existingEntry.SetDate = DateTime.Now;
                }
                else
                {
                    var newEmployeeEducationInfo = new EmployeeEducationInfo()
                    {
                        IdClient = item.IdClient,
                        IdEmployee = existingEmployee.Id,
                        IdEducationLevel = item.IdEducationLevel,
                        IdEducationExamination = item.IdEducationExamination,
                        IdEducationResult = item.IdEducationResult,
                        Cgpa = item.Cgpa,
                        Marks = item.Marks,
                        PassingYear = item.PassingYear,
                        InstituteName = item.InstituteName,
                        Major = item.Major,
                        IsForeignInstitute = item.IsForeignInstitute,
                        Duration = item.Duration,
                        Achievement = item.Achievement,
                        SetDate = DateTime.Now
                    };

                    existingEmployee.EmployeeEducationInfos.Add(newEmployeeEducationInfo);
                }
            }


            foreach (var item in employee.FamilyInfos)
            {
                var existingEntry = existingEmployee.EmployeeFamilyInfos.FirstOrDefault(ei => ei.IdClient == item.IdClient && ei.Id == item.Id);
                if (existingEntry != null)
                {
                    existingEntry.IdGender = item.IdGender;
                    existingEntry.IdRelationship = item.IdRelationship;
                    existingEntry.Name = item.Name;
                    existingEntry.DateOfBirth = item.DateOfBirth;
                    existingEntry.ContactNo = item.ContactNo;
                    existingEntry.CurrentAddress = item.CurrentAddress;
                    existingEntry.PermanentAddress = item.PermanentAddress;
                    existingEntry.SetDate = DateTime.Now;
                }
                else
                {
                    var newEmployeeFamilyInfo = new EmployeeFamilyInfo()
                    {
                        IdClient = item.IdClient,
                        IdEmployee = existingEmployee.Id,
                        IdGender = item.IdGender,
                        IdRelationship = item.IdRelationship,
                        Name = item.Name,
                        DateOfBirth = item.DateOfBirth,
                        ContactNo = item.ContactNo,
                        CurrentAddress = item.CurrentAddress,
                        PermanentAddress=item.PermanentAddress,
                        SetDate = DateTime.Now
                    };

                    existingEmployee.EmployeeFamilyInfos.Add(newEmployeeFamilyInfo);
                }
            }


            foreach (var item in employee.Certifications)
            {
                var existingEntry = existingEmployee.EmployeeProfessionalCertifications.FirstOrDefault(ei => ei.IdClient == item.IdClient && ei.Id == item.Id);
                if (existingEntry != null)
                {
                    existingEntry.CertificationTitle = item.CertificationTitle;
                    existingEntry.CertificationInstitute = item.CertificationInstitute;
                    existingEntry.InstituteLocation = item.InstituteLocation;
                    existingEntry.FromDate = item.FromDate;
                    existingEntry.ToDate = item.ToDate;
                    existingEntry.SetDate = DateTime.Now;
                }
                else
                {
                    var newCertification = new EmployeeProfessionalCertification
                    {
                        IdClient = item.IdClient,
                        IdEmployee = existingEmployee.Id,
                        CertificationTitle = item.CertificationTitle,
                        CertificationInstitute = item.CertificationInstitute,
                        InstituteLocation = item.InstituteLocation,
                        FromDate = item.FromDate,
                        ToDate = item.ToDate,
                        SetDate = DateTime.Now
                    };
                    existingEmployee.EmployeeProfessionalCertifications.Add(newCertification);
                }
            }

            var result = await Context.SaveChangesAsync(cancellationToken);

            return result;
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
        public async Task<bool> SoftDeleteAsync(int idClient, int id,CancellationToken cancellationToken)
        {
            //var employee = await Context.Employees.FirstOrDefaultAsync(e => e.IdClient == idClient && e.Id == id, cancellationToken);
            ////.FirstOrDefaultAsync(e => e.IdClient == 10001001 && e.Id == id);

            //if (employee == null)
            //{
            //    return false;
            //}
            //employee.IsActive = false;
            //await Context.SaveChangesAsync(cancellationToken);

            var affectedRows = await Context.Employees
           .Where(e => e.IdClient == idClient && e.Id == id)
           .ExecuteUpdateAsync(update => update
           .SetProperty(emp => emp.IsActive, false)
           .SetProperty(emp => emp.SetDate, DateTime.Now),
           cancellationToken);

            return affectedRows > 0;
        }

       
    }

}

