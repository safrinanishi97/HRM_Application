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
        Task<Employee?> GetByIdAsync(int idClient, int id, CancellationToken cancellationToken);
        Task<Employee?> GetByIdForUpdate(int idClient, int id, CancellationToken cancellationToken);
        Task<List<Employee>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> CreateAsync(Employee employee, CancellationToken cancellationToken);
        Task<Employee> UpdateAsync(Employee employee, CancellationToken cancellationToken);
        Task<bool> SoftDeleteAsync(int idClient, int id, CancellationToken cancellationToken);
    }
}
