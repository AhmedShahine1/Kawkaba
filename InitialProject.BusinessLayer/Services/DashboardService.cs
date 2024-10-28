using Kawkaba.BusinessLayer.Interfaces;
using Kawkaba.Core;
using Kawkaba.RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kawkaba.BusinessLayer.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> GetCompanyUserCountAsync()
        {
            var companyRoleId = _unitOfWork.RoleRepository
                .Find(r => r.Name == "Company")
                .Id;

            return _unitOfWork.UserRoleRepository
                .FindAll(ur => ur.RoleId == companyRoleId)
                .Count();
        }

        public async Task<int> GetEmployeeUserCountAsync()
        {
            var employeeRoleId = _unitOfWork.RoleRepository
                .Find(r => r.Name == "Employee")
                .Id;

            return _unitOfWork.UserRoleRepository
                .FindAll(ur => ur.RoleId == employeeRoleId)
                .Count();
        }

        public async Task<int> GetUsersWithoutCompanyCountAsync()
        {
            var companyRoleId = _unitOfWork.RoleRepository
                .Find(r => r.Name == "Company")
                .Id;

            return await _unitOfWork.UserRepository
                .CountAsync(u => !_unitOfWork.UserRoleRepository
                    .IsExist(ur => ur.UserId == u.Id && ur.RoleId == companyRoleId));
        }

        public async Task<int> GetPostCountAsync()
        {
            return await _unitOfWork.PostRepository.CountAsync();
        }
    }
}
