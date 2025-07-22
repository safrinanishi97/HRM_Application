using HRMApiApp.DAL.Interfaces;
using HRMApiApp.DTO;
using HRMApiApp.Model;
using HRMApiApp.Models;
using HRMApiApp.ViewModels;
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


    public class EmployeeRepository(HanaHrmContext context) : IEmployeeRepository
    {
        private readonly HanaHrmContext _context = context;

        public async Task<Employee?> GetByIdAsync(int idClient, int id, CancellationToken cancellationToken)
        {
            var emp = await _context.Employees
              .AsNoTracking()
                .Include(e => e.EmployeeDocuments)
                .Include(e => e.EmployeeEducationInfos)
                .Include(e => e.EmployeeProfessionalCertifications).FirstOrDefaultAsync(e => e.IdClient == idClient && e.Id == id, cancellationToken);
            return emp;
        }

        public async Task<List<Employee>> GetAllAsync(CancellationToken cancellationToken)
        {
            var emp= await _context.Employees
                .AsNoTracking()
                .Include(e => e.EmployeeDocuments)
                .Include(e => e.EmployeeEducationInfos)
                .Include(e => e.EmployeeProfessionalCertifications)
                .Where(e => e.IsActive != false)
                .ToListAsync(cancellationToken);
            return emp;
        }

        public async Task<bool> CreateAsync(Employee employee, CancellationToken cancellationToken)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<int> UpdateAsync(EmployeeUpdateDTO employee, CancellationToken cancellationToken)
        {
            async Task<byte[]?> ConvertFileToByteArrayAsync(IFormFile? file)
            {
                if (file == null || file.Length == 0)
                    return null;

                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
            if (employee == null)
                throw new Exception($"data not found: {nameof(employee)}");

            var idClient = employee.IdClient;
            var id = employee.Id;

            var existingEmployee = await _context.Employees
                .Include(e => e.EmployeeDocuments)
                .Include(e => e.EmployeeEducationInfos)
                .Include(e => e.EmployeeProfessionalCertifications)
                .FirstOrDefaultAsync(e => e.IdClient == idClient && e.Id == id, cancellationToken);

            if (existingEmployee == null) return 0;

            existingEmployee.EmployeeName = employee.EmployeeName ?? existingEmployee.EmployeeName;
            existingEmployee.EmployeeNameBangla = employee.EmployeeNameBangla ?? existingEmployee.EmployeeNameBangla;
            existingEmployee.FatherName = employee.FatherName ?? existingEmployee.FatherName;
            existingEmployee.MotherName = employee.MotherName ?? existingEmployee.MotherName;
            existingEmployee.EmployeeImage = await ConvertFileToByteArrayAsync(employee.ProfileImage) ?? existingEmployee.EmployeeImage;
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
                .Where(ed => ed.IdClient == ed.IdClient && !employee.Documents.Any(d => d.IdClient == ed.IdClient && d.Id == ed.Id))
                .ToList();
            if(deletedEmployeeDocumentList!=null)
            {
                _context.EmployeeDocuments.RemoveRange(deletedEmployeeDocumentList);
            }

            var deletedEmployeeEducationInfoList = existingEmployee.EmployeeEducationInfos
                .Where(eei => eei.IdClient == eei.IdClient && !employee.EducationInfos.Any(ei => ei.IdClient == eei.IdClient && ei.Id == eei.Id))
                .ToList();
            if (deletedEmployeeEducationInfoList != null)
            {
                _context.EmployeeEducationInfos.RemoveRange(deletedEmployeeEducationInfoList);
            }

            var deletedCertificationList = existingEmployee.EmployeeProfessionalCertifications
                .Where(epc => epc.IdClient == epc.IdClient && !employee.Certifications.Any(c => c.IdClient == epc.IdClient && c.Id == epc.Id))
                .ToList();

            if (deletedCertificationList != null) 
            {
                _context.EmployeeProfessionalCertifications.RemoveRange(deletedCertificationList);
            }


            //up/insert information
            
            foreach(var item in employee.Documents)
            {
                var existingEntry = existingEmployee.EmployeeDocuments.FirstOrDefault(ed=>ed.IdClient == item.IdClient && ed.Id == item.Id);
                var uploadedBytes = await ConvertFileToByteArrayAsync(item.File);
                var extension = Path.GetExtension(item.File?.FileName);
                if (existingEntry != null) 
                {
                    existingEntry.DocumentName = item.DocumentName;
                    existingEntry.FileName = item.FileName;
                    existingEntry.UploadDate = item.UploadDate;
                    existingEntry.UploadedFileExtention = item.UploadedFileExtention;
                    existingEntry.UploadedFile = uploadedBytes ?? existingEntry.UploadedFile;
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

            var result = await _context.SaveChangesAsync();

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
        public async Task<bool> SoftDeleteAsync(int idClient, int id, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.IdClient == idClient && e.Id == id);
            //.FirstOrDefaultAsync(e => e.IdClient == 10001001 && e.Id == id);

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

