using HRMApiApp.Models;
using HRMApiApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMApiApp.DAL.Interfaces
{
    public interface IEmployeeRepository
    {
        List<EmployeeViewModel> GetAllEmployees();
        Task<Employee?> GetEmployeeById(int idClient, int id);
        Task<Employee?> AddEmployee(Employee employee);

        Task<int> SaveChangesAsync();
        Task<Employee?> GetEmployeeForUpdate(int idClient, int id);
        Task<bool> DeleteEmployee(int idClient, int id);
    }
}
