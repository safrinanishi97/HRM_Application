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
    public class CommonService(ICommonRepository _commonRepository) : ICommonService
    {

        public async Task<List<CommonViewModel>> GetAllDepartment(int idClient)
        {
            return await _commonRepository.GetAllDepartment(idClient);
        }

        public async Task<List<CommonViewModel>> GetAllDesignation(int idClient)
        {
            return await _commonRepository.GetAllDesignation(idClient);
        }

        public async Task<List<CommonViewModel>> GetAllEducationLevel(int idClient)
        {
            return await _commonRepository.GetAllEducationLevel(idClient);
        }

        public async Task<List<CommonViewModel>> GetAllEducationResult(int idClient)
        {
            return await _commonRepository.GetAllEducationResult(idClient);
        }

        public async Task<List<CommonViewModel>> GetAllEmployeeType(int idClient)
        {   
            return await _commonRepository.GetAllEmployeeType(idClient);
        }

        public async Task<List<CommonViewModel>> GetAllGender(int idClient)
        {
            return await _commonRepository.GetAllGender(idClient);
        }

        public async Task<List<CommonViewModel>> GetAllJobType(int idClient)
        {
            return await _commonRepository.GetAllJobType(idClient);
        }

        public async Task<List<CommonViewModel>> GetAllMaritalStatus(int idClient)
        {
            return await _commonRepository.GetAllMaritalStatus(idClient);
        }

        public async Task<List<CommonViewModel>> GetAllRelationship(int idClient)
        {
            return await _commonRepository.GetAllRelationship(idClient);
        }

        public async Task<List<CommonViewModel>> GetAllReligion(int idClient)
        {
            return await _commonRepository.GetAllReligion(idClient);
        }

        public async Task<List<CommonViewModel>> GetAllWeekOff(int idClient)
        {
            return await _commonRepository.GetAllWeekOff(idClient);
        }
    }
}
