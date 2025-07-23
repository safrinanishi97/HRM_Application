using HRMApiApp.DTO;
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
        Task<Employee?> GetByIdAsync(int idClient, int id);
        Task<List<Employee>> GetAllAsync(int idClient);
        Task<bool> CreateAsync(Employee employee);
        Task<int> UpdateAsync(EmployeeUpdateDTO employeeDto);
        Task<bool> SoftDeleteAsync(int idClient, int id);
    }
}
