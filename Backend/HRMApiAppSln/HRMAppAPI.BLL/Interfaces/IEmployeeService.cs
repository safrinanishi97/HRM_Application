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

        Task<EmployeeDTO?> GetByIdAsync(int idClient,int id);
        Task<List<EmployeeDTO>> GetAllAsync(int idClient);
        Task<bool> CreateAsync(EmployeeCreateDTO employeeDto);
        Task<string> UpdateAsync(EmployeeUpdateDTO employeeDto);
        Task<bool> DeleteAsync(int idClient, int idn);
        Task<(byte[]? fileData, string mimeType)> GetEmployeeFileAsync(int idClient, int id, string fileType, int? documentId);
    }
}
