using HRMApiApp.BLL.Interfaces;
using HRMApiApp.DAL;
using HRMApiApp.DAL.Interfaces;
using HRMApiApp.DTO;
using HRMApiApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMApiApp.BLL
{
    public class CommonService(ICommonRepository commonRepository) : ICommonService
    {
        private readonly ICommonRepository _commonRepository = commonRepository;

        
        public async Task<List<CommonViewModel>> GetAllDepartment(CancellationToken cancellationToken)
        {
            return await _commonRepository.GetAllDepartment(cancellationToken);
        }

        public async Task<List<CommonViewModel>> GetAllDesignation(CancellationToken cancellationToken)
        {
            return await _commonRepository.GetAllDesignation(cancellationToken);
        }

        public async Task<List<CommonViewModel>> GetAllEducationLevel(CancellationToken cancellationToken)
        {
            return await _commonRepository.GetAllEducationLevel(cancellationToken);
        }

        public async Task<List<CommonViewModel>> GetAllEducationResult(CancellationToken cancellationToken)
        {
            return await _commonRepository.GetAllEducationResult(cancellationToken);
        }

        public async Task<List<CommonViewModel>> GetAllEmployeeType(CancellationToken cancellationToken)
        {   
            return await _commonRepository.GetAllEmployeeType(cancellationToken);
        }

        public async Task<List<CommonViewModel>> GetAllGender(CancellationToken cancellationToken)
        {
            return await _commonRepository.GetAllGender(cancellationToken);
        }

        public async Task<List<CommonViewModel>> GetAllJobType(CancellationToken cancellationToken)
        {
            return await _commonRepository.GetAllJobType(cancellationToken);
        }

        public async Task<List<CommonViewModel>> GetAllMaritalStatus(CancellationToken cancellationToken)
        {
            return await _commonRepository.GetAllMaritalStatus(cancellationToken);
        }

        public async Task<List<CommonViewModel>> GetAllRelationship(CancellationToken cancellationToken)
        {
            return await _commonRepository.GetAllRelationship(cancellationToken);
        }

        public async Task<List<CommonViewModel>> GetAllReligion(CancellationToken cancellationToken)
        {
            return await _commonRepository.GetAllReligion(cancellationToken);
        }

        public async Task<List<CommonViewModel>> GetAllWeekOff(CancellationToken cancellationToken)
        {
            return await _commonRepository.GetAllWeekOff(cancellationToken);
        }
    }
}
