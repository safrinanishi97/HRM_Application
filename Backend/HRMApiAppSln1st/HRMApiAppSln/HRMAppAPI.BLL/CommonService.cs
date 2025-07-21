using HRMApiApp.BLL.Interfaces;
using HRMApiApp.DAL;
using HRMApiApp.DAL.Interfaces;
using HRMApiApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMApiApp.BLL
{
    public class CommonService:ICommonService
    {
        private readonly ICommonRepository _commonRepository;
        public CommonService(ICommonRepository commonRepository)
        {
            _commonRepository = commonRepository;
        }

        public List<CommonViewModel> GetAllDepartment()
        {
            return _commonRepository.GetAllDepartment();
        }

        public List<CommonViewModel> GetAllDesignation()
        {
            return _commonRepository.GetAllDesignation();
        }

        public List<CommonViewModel> GetAllEducationLevel()
        {
            return _commonRepository.GetAllEducationLevel();
        }

        public List<CommonViewModel> GetAllEducationResult()
        {
            return _commonRepository.GetAllEducationResult();
        }

        public List<CommonViewModel> GetAllEmployeeType()
        {
            return _commonRepository.GetAllEmployeeType();
        }

        public List<CommonViewModel> GetAllGender()
        {
            return _commonRepository.GetAllGender();
        }

        public List<CommonViewModel> GetAllJobType()
        {
            return _commonRepository.GetAllJobType();
        }

        public List<CommonViewModel> GetAllMaritalStatus()
        {
            return _commonRepository.GetAllMaritalStatus();
        }

        public List<CommonViewModel> GetAllRelationship()
        {
            return _commonRepository.GetAllRelationship();
        }

        public List<CommonViewModel> GetAllReligion()
        {
            return _commonRepository.GetAllReligion();
        }

        public List<CommonViewModel> GetAllWeekOff()
        {
            return _commonRepository.GetAllWeekOff();
        }
    }
}
