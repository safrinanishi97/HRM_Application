using HRMApiApp.DTO;
using HRMApiApp.Models;
using HRMApiApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMApiApp.BLL.Interfaces
{
    public interface IEmployeeService
    {
        List<EmployeeViewModel> GetAllEmployees();
        Task<Employee?> GetEmployeeById(int idClient, int id);
        Task<Employee?> CreateEmployeeWithDetails(EmployeeDetailDTO employeeDetail);

        Task<bool> UpdateEmployee(EmployeeDTO dto);

        Task<bool> DeleteEmployee(int idClient, int id);
    }
}
