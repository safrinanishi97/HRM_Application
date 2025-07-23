using HRMApiApp.Models;
using HRMApiApp.ViewModel;
using HRMApiApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMApiApp.DAL.Interfaces
{
    public interface ICommonRepository
    {
       
        Task<List<CommonViewModel>> GetAllDepartment(CancellationToken cancellationToken);
        Task<List<CommonViewModel>> GetAllDesignation(CancellationToken cancellationToken);
        Task<List<CommonViewModel>> GetAllEducationLevel(CancellationToken cancellationToken);
        Task<List<CommonViewModel>> GetAllEducationResult(CancellationToken cancellationToken);
        Task<List<CommonViewModel>> GetAllEmployeeType(CancellationToken cancellationToken);
        Task<List<CommonViewModel>> GetAllGender(CancellationToken cancellationToken);
        Task<List<CommonViewModel>> GetAllJobType(CancellationToken cancellationToken);
        Task<List<CommonViewModel>> GetAllMaritalStatus(CancellationToken cancellationToken);
        Task<List<CommonViewModel>> GetAllRelationship(CancellationToken cancellationToken);
        Task<List<CommonViewModel>> GetAllReligion(CancellationToken cancellationToken);
        Task<List<CommonViewModel>> GetAllWeekOff(CancellationToken cancellationToken); 

    }
}
