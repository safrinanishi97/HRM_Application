using HRMApiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HRMApiApp.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public int IdClient { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmployeeNameBangla { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public int? IdReportingManager { get; set; }
        public int? IdJobType { get; set; }
        public int? IdEmployeeType { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? JoiningDate { get; set; }
        public int? IdGender { get; set; }
        public int? IdReligion { get; set; }
        public int IdDepartment { get; set; }
        public int IdSection { get; set; }
        public int? IdDesignation { get; set; }
        public bool? HasOvertime { get; set; }
        public bool? HasAttendenceBonus { get; set; }
        public int? IdWeekOff { get; set; }
        public string? Address { get; set; }
        public string? PresentAddress { get; set; }
        public string? NationalIdentificationNumber { get; set; }
        public string? ContactNo { get; set; }
        public int? IdMaritalStatus { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? SetDate { get; set; }
        public string? CreatedBy { get; set; }

    }
    public class EmployeeDetailDTO
    {
        public int Id { get; set; }
        public int IdClient { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmployeeNameBangla { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public int? IdReportingManager { get; set; }
        public int? IdJobType { get; set; }
        public int? IdEmployeeType { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? JoiningDate { get; set; }
        public int? IdGender { get; set; }
        public int? IdReligion { get; set; }
        public int IdDepartment { get; set; }
        public int IdSection { get; set; }
        public int? IdDesignation { get; set; }
        public bool? HasOvertime { get; set; }
        public bool? HasAttendenceBonus { get; set; }
        public int? IdWeekOff { get; set; }
        public string? Address { get; set; }
        public string? PresentAddress { get; set; }
        public string? NationalIdentificationNumber { get; set; }
        public string? ContactNo { get; set; }
        public int? IdMaritalStatus { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? SetDate { get; set; }
        public string? CreatedBy { get; set; }
        public List<EmployeeDocumentDTO> Documents { get; set; } = new();
        public List<EmployeeEducationDTO> Educations { get; set; } = new();
        public List<EmployeeCertificationDTO> Certifications { get; set; } = new();
    }

    public class EmployeeDocumentDTO
    {
        public int IdClient { get; set; }

        public int Id { get; set; }

        public int IdEmployee { get; set; }

        public string DocumentName { get; set; } = null!;

        public string FileName { get; set; } = null!;

        public DateTime UploadDate { get; set; }

        public string? UploadedFileExtention { get; set; }

        public byte[]? UploadedFile { get; set; }

        public DateTime? SetDate { get; set; }

        public string? CreatedBy { get; set; }

        //public virtual Employee Employee { get; set; } = null!;
    }

    public class EmployeeEducationDTO
    {
        public int IdClient { get; set; }

        public int Id { get; set; }

        public int IdEmployee { get; set; }

        public int IdEducationLevel { get; set; }

        public int IdEducationExamination { get; set; }

        public int IdEducationResult { get; set; }

        public decimal? Cgpa { get; set; }

        public decimal? ExamScale { get; set; }

        public decimal? Marks { get; set; }

        public string Major { get; set; } = null!;

        public decimal PassingYear { get; set; }

        public string InstituteName { get; set; } = null!;

        public bool IsForeignInstitute { get; set; }

        public decimal? Duration { get; set; }

        public string? Achievement { get; set; }

        public DateTime? SetDate { get; set; }

        public string? CreatedBy { get; set; }

        //public virtual EducationExamination EducationExamination { get; set; } = null!;

       //public virtual EducationLevel EducationLevel { get; set; } = null!;

       // public virtual EducationResult EducationResult { get; set; } = null!;

        //public virtual Employee Employee { get; set; } = null!;

    }
    public class EmployeeCertificationDTO
    {
        public int IdClient { get; set; }

        public int Id { get; set; }

        public int IdEmployee { get; set; }

        public string CertificationTitle { get; set; } = null!;

        public string CertificationInstitute { get; set; } = null!;

        public string InstituteLocation { get; set; } = null!;

        public DateTime FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public DateTime? SetDate { get; set; }

        public string? CreatedBy { get; set; }

        //public virtual Employee Employee { get; set; } = null!;
    }



}
