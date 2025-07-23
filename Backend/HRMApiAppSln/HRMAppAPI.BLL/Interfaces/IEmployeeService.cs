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

        Task<EmployeeDTO?> GetByIdAsync(int id, int idClient, CancellationToken cancellationToken);
        Task<List<EmployeeDTO>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> CreateAsync(EmployeeCreateDTO employeeDto, CancellationToken cancellationToken);
        Task<string> UpdateAsync(EmployeeUpdateDTO employeeDto, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int idClient, int id, CancellationToken cancellationToken);


        Task<(byte[]? fileData, string mimeType)> GetEmployeeFileAsync(int idClient, int id, string fileType, int? documentId,
            CancellationToken cancellationToken);
    }
}
