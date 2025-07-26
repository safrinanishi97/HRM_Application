using HRMApiApp.DTO;
using HRMApiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMApiApp.DAL.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetByIdAsync(int idClient, int id,CancellationToken cancellationToken);
        Task<List<Employee>> GetAllAsync(int idClient, CancellationToken cancellationToken);
        Task<bool> CreateAsync(Employee employee, CancellationToken cancellationToken);
        Task<int> UpdateAsync(EmployeeUpdateDTO employeeDto, CancellationToken cancellationToken);
        Task<bool> SoftDeleteAsync(int idClient, int id, CancellationToken cancellationToken);
    }
}
