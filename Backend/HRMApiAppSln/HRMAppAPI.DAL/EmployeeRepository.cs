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
            var existingEmployee = await _context.Employees
        .Include(e => e.EmployeeDocuments)
        .Include(e => e.EmployeeEducationInfos)
        .Include(e => e.EmployeeProfessionalCertifications)
        .FirstOrDefaultAsync(e => e.IdClient == employee.IdClient && e.Id == employee.Id, cancellationToken);

            if (existingEmployee == null)
                throw new Exception("Employee not found");


            _context.Entry(existingEmployee).CurrentValues.SetValues(employee);

            _context.EmployeeDocuments.RemoveRange(existingEmployee.EmployeeDocuments
                .Where(ed => !employee.EmployeeDocuments.Any(d => d.IdClient == ed.IdClient && d.Id == ed.Id)));

            foreach (var doc in employee.EmployeeDocuments)
            {
                var existingDoc = existingEmployee.EmployeeDocuments
                    .FirstOrDefault(ed => ed.IdClient == doc.IdClient && ed.Id == doc.Id);

                if (existingDoc != null)
                {
                    _context.Entry(existingDoc).CurrentValues.SetValues(doc);
                }
                else
                {
                    existingEmployee.EmployeeDocuments.Add(doc);
                }
            }

            _context.EmployeeEducationInfos.RemoveRange(existingEmployee.EmployeeEducationInfos
                .Where(edu => !employee.EmployeeEducationInfos.Any(d => d.IdClient == edu.IdClient && d.Id == edu.Id)));

            foreach (var edu in employee.EmployeeEducationInfos)
            {
                var existingEdu = existingEmployee.EmployeeEducationInfos
                    .FirstOrDefault(e => e.IdClient == edu.IdClient && e.Id == edu.Id);

                if (existingEdu != null)
                {
                    _context.Entry(existingEdu).CurrentValues.SetValues(edu);
                }
                else
                {
                    existingEmployee.EmployeeEducationInfos.Add(edu);
                }
            }

            _context.EmployeeProfessionalCertifications.RemoveRange(existingEmployee.EmployeeProfessionalCertifications
                .Where(cert => !employee.EmployeeProfessionalCertifications.Any(d => d.IdClient == cert.IdClient && d.Id == cert.Id)));

            foreach (var cert in employee.EmployeeProfessionalCertifications)
            {
                var existingCert = existingEmployee.EmployeeProfessionalCertifications
                    .FirstOrDefault(c => c.IdClient == cert.IdClient && c.Id == cert.Id);

                if (existingCert != null)
                {
                    _context.Entry(existingCert).CurrentValues.SetValues(cert);
                }
                else
                {
                    existingEmployee.EmployeeProfessionalCertifications.Add(cert);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            return existingEmployee;
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

