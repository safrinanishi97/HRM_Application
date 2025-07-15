using HRMApiApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMApiApp.BLL.Interfaces
{
    public interface ICommonService
    {
        List<CommonViewModel> GetAllDepartment();
        List<CommonViewModel> GetAllDesignation();
        List<CommonViewModel> GetAllEducationLevel();
        List<CommonViewModel> GetAllEducationResult();
        List<CommonViewModel> GetAllEmployeeType();
        List<CommonViewModel> GetAllGender();
        List<CommonViewModel> GetAllJobType();
        List<CommonViewModel> GetAllMaritalStatus();
        List<CommonViewModel> GetAllRelationship();
        List<CommonViewModel> GetAllReligion();
        List<CommonViewModel> GetAllWeekOff();
    }
}
