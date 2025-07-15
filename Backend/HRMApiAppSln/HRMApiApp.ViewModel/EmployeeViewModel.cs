using HRMApiApp.Models;
using HRMApiApp.ViewModels;

namespace HRMApiApp.ViewModels
{
    public class EmployeeViewModel
    {
        public int IdClient { get; set; }

        public int Id { get; set; }

        public string? EmployeeName { get; set; }

        public string? EmployeeNameBangla { get; set; }

        public byte[]? EmployeeImage { get; set; }

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

        public string JobTypeName { get; set; } = null!;

        public string? JobTypeBanglaName { get; set; }

        public string MaritalStatusName { get; set; } = null!;

        public string ReligionName { get; set; } = null!;

        public string SectionName { get; set; } = null!;

        public string? SectionNameBangla { get; set; }

        public string ShortName { get; set; } = null!;

        public string? WeekOffDay { get; set; }

        public string DepartName { get; set; } = null!;

        public string? DepartNameBangla { get; set; }

        public string DesignationName { get; set; } = null!;

        public string? DesignationNameBangla { get; set; }

        public string? GenderName { get; set; }

    }
}
