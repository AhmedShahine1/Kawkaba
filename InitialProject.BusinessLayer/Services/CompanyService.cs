using AutoMapper;
using Kawkaba.BusinessLayer.AutoMapper;
using Kawkaba.BusinessLayer.Interfaces;
using Kawkaba.Core.DTO.AuthViewModel;
using Kawkaba.Core.DTO.AuthViewModel.RequestEmployeeModel;
using Kawkaba.Core.Entity.ApplicationData;
using Kawkaba.Core.Entity.RequestEmployee;
using Kawkaba.Core.Helpers;
using Kawkaba.RepositoryLayer.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Kawkaba.BusinessLayer.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper autoMapper;
        private readonly IFileHandling _fileHandling;

        public CompanyService(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IMapper autoMapper, IFileHandling fileHandling)
        {
            _userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.autoMapper = autoMapper;
            _fileHandling = fileHandling;
        }

        public async Task<bool> RequestToJoinCompany(string employeeId, int companyCode)
        {
            try
            {
                var employee = await _userManager.FindByIdAsync(employeeId);
                var company = await unitOfWork.UserRepository.FindAsync(a => a.CompanyCode == companyCode);

                if (employee == null)
                {
                    throw new ArgumentNullException("Not Found Employee");
                }
                if (company == null)
                {
                    throw new ArgumentNullException("Not Found Company");
                }
                var validrequest = unitOfWork.RequestEmployeeRepository.IsExist(a => a.CompanyId == company.CompanyId && a.EmployeeId == employeeId);
                if (validrequest == false)
                {
                    var requestEmployee = new RequestEmployee()
                    {
                        CompanyId = company.Id,
                        EmployeeId = employeeId,
                        Employee = employee,
                        Company = company
                    };
                    await unitOfWork.RequestEmployeeRepository.AddAsync(requestEmployee);
                    await unitOfWork.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new DbUpdateException(ex.Message);
            }
        }

        public async Task<bool> UpdateStatusRequest(string requestId, StatusRequestEmployee statusRequestEmployee)
        {
            try
            {
                var requestEmployee = await unitOfWork.RequestEmployeeRepository.FindAsync(a => a.Id == requestId);
                if (requestEmployee == null)
                {
                    throw new ArgumentNullException("Not Found Request Employee");
                }
                var employee = await _userManager.FindByIdAsync(requestEmployee.EmployeeId);
                if (employee == null)
                {
                    throw new ArgumentNullException("Not Found Employee");
                }

                var company = await _userManager.FindByIdAsync(requestEmployee.CompanyId);
                if (company == null)
                {
                    throw new ArgumentNullException("Not Found Company");
                }
                if (StatusRequestEmployee.Accapted == statusRequestEmployee)
                {
                    employee.CompanyId = company.Id;
                    company.Employee.Add(employee);
                    await _userManager.UpdateAsync(company);
                    var result = await _userManager.UpdateAsync(employee);
                    requestEmployee.StatusRequestEmployee = statusRequestEmployee;
                }
                unitOfWork.RequestEmployeeRepository.Delete(requestEmployee);
                await unitOfWork.SaveChangesAsync();
                await _userManager.UpdateAsync(company);
                return true;
            }
            catch (Exception ex)
            {
                throw new DbUpdateException(ex.Message);
            }
        }

        public async Task<List<ApplicationUser>> GetCompanyEmployees(string companyId)
        {
            return await _userManager.Users
                .Where(u => u.CompanyId == companyId).ToListAsync();
        }

        public async Task<List<RequestEmployeeDTO>> GetRequestCompany(string companyId)
        {
            // Fetch the request employees for the given companyId
            var requests = await unitOfWork.RequestEmployeeRepository.FindAllAsync(
                u => u.CompanyId == companyId && u.StatusRequestEmployee != StatusRequestEmployee.Canceled,
                include: q => q
                    .Include(a => a.Employee)   // Includes Employee entity
                    .Include(a => a.Company)    // Includes Company entity
            );

            // Create a list of RequestEmployeeDTO to store the result
            var requestsDTO = new List<RequestEmployeeDTO>();

            // Loop through each request and map manually
            foreach (var request in requests)
            {
                var requestDTO = new RequestEmployeeDTO
                {
                    Id = request.Id,
                    Employee = autoMapper.Map<AuthDTO>(request.Employee),
                };
                requestDTO.Employee.ProfileImage = await _fileHandling.GetFile(request.Employee.ProfileId);

                // Add the mapped DTO to the list
                requestsDTO.Add(requestDTO);
            }

            // Return the manually mapped list of RequestEmployeeDTO
            return requestsDTO;
        }

        public async Task<bool> RemoveEmployee(string employeeId)
        {
            try
            {
                var employee = await _userManager.FindByIdAsync(employeeId);
                employee.CompanyId = null;
                await _userManager.UpdateAsync(employee);
                var result = await _userManager.UpdateAsync(employee);
                unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
