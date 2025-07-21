using HRMApiApp.DAL.Interfaces;
using HRMApiApp.Model;
using HRMApiApp.Models;
using HRMApiApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HRMApiApp.DAL
{


    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HanaHrmContext _context;

        public EmployeeRepository(HanaHrmContext context)
        {
            _context = context;
        }

        public async Task<Employee?> GetByIdAsync(int idClient, int id, CancellationToken cancellationToken)
        {
            var emp = await _context.Employees
              .AsNoTracking()
                .Include(e => e.EmployeeDocuments)
                .Include(e => e.EmployeeEducationInfos)
                .Include(e => e.EmployeeProfessionalCertifications).FirstOrDefaultAsync(e => e.IdClient == idClient && e.Id == id, cancellationToken);
            return emp;
        }

        public async Task<List<Employee>> GetAllAsync(CancellationToken cancellationToken)
        {
            var emp= await _context.Employees
                .AsNoTracking()
                .Include(e => e.EmployeeDocuments)
                .Include(e => e.EmployeeEducationInfos)
                .Include(e => e.EmployeeProfessionalCertifications)
                .Where(e => e.IsActive != false)
                .ToListAsync(cancellationToken);
            return emp;
        }

        public async Task<bool> CreateAsync(Employee employee, CancellationToken cancellationToken)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<Employee?> GetByIdForUpdate(int idClient, int id, CancellationToken cancellationToken)
        {
            var employee= await _context.Employees
                .Include(e => e.EmployeeDocuments)
                .Include(e => e.EmployeeEducationInfos)
                .Include(e => e.EmployeeProfessionalCertifications).FirstOrDefaultAsync(e => e.IdClient == idClient && e.Id == id, cancellationToken);
            return employee;
        }
        public async Task<Employee> UpdateAsync(Employee employee, CancellationToken cancellationToken)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync(cancellationToken);
            return employee;
        }
        //Hard delete
        //public bool DeleteEmployee(int id)
        //{
        //    var employee = _context.Employees.FirstOrDefault(s=>s.Id==id);
        //    if (employee == null)
        //    {
        //        return false; 
        //    }
        //    _context.Employees.Remove(employee);
        //    _context.SaveChanges();
        //    return true;

        //}

        //soft delete
        public async Task<bool> SoftDeleteAsync(int idClient, int id,CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.IdClient == idClient && e.Id == id);
            //.FirstOrDefaultAsync(e => e.IdClient == 10001001 && e.Id == id);

            if (employee == null)
            {
                return false;
            }
            employee.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

       
    }

}

