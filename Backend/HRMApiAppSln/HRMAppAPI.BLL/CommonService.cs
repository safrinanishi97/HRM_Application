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
    public class CommonService(ICommonRepository CommonRepository) : ICommonService
    {

        public async Task<List<CommonViewModel>> GetAllDepartment(int idClient)
        {
            return await CommonRepository.GetAllDepartment(idClient);
        }

        public async Task<List<CommonViewModel>> GetAllDesignation(int idClient)
        {
            return await CommonRepository.GetAllDesignation(idClient);
        }

        public async Task<List<CommonViewModel>> GetAllEducationLevel(int idClient)
        {
            return await CommonRepository.GetAllEducationLevel(idClient);
        }

        public async Task<List<CommonViewModel>> GetAllEducationResult(int idClient)
        {
            return await CommonRepository.GetAllEducationResult(idClient);
        }

        public async Task<List<CommonViewModel>> GetAllEmployeeType(int idClient)
        {   
            return await CommonRepository.GetAllEmployeeType(idClient);
        }

        public async Task<List<CommonViewModel>> GetAllGender(int idClient)
        {
            return await CommonRepository.GetAllGender(idClient);
        }

        public async Task<List<CommonViewModel>> GetAllJobType(int idClient)
        {
            return await CommonRepository.GetAllJobType(idClient);
        }

        public async Task<List<CommonViewModel>> GetAllMaritalStatus(int idClient)
        {
            return await CommonRepository.GetAllMaritalStatus(idClient);
        }

        public async Task<List<CommonViewModel>> GetAllRelationship(int idClient)
        {
            return await CommonRepository.GetAllRelationship(idClient);
        }

        public async Task<List<CommonViewModel>> GetAllReligion(int idClient)
        {
            return await CommonRepository.GetAllReligion(idClient);
        }

        public async Task<List<CommonViewModel>> GetAllWeekOff(int idClient)
        {
            return await CommonRepository.GetAllWeekOff(idClient);
        }
    }
}
