using Kawkaba.BusinessLayer.Interfaces;
using Kawkaba.Core;
using Kawkaba.Core.DTO.AuthViewModel;
using Kawkaba.Core.DTO.AuthViewModel.PostsModel;
using Kawkaba.Core.Entity.ApplicationData;
using Kawkaba.Core.Entity.Posts;
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
        private readonly IAccountService _accountService;
        private readonly IFileHandling _fileService;

        public DashboardService(IUnitOfWork unitOfWork, IAccountService accountService, IFileHandling fileService)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
            _fileService = fileService;
        }

        public int GetCompanyUserCountAsync()
        {
            var companyRoleId = _unitOfWork.RoleRepository
                .Find(r => r.Name == "Company")
                .Id;

            return _unitOfWork.UserRoleRepository
                .FindAll(ur => ur.RoleId == companyRoleId)
                .Count();
        }

        public int GetEmployeeUserCountAsync()
        {
            var employeeRoleId = _unitOfWork.RoleRepository
                .Find(r => r.Name == "Employee")
                .Id;

            return _unitOfWork.UserRoleRepository
                .FindAll(ur => ur.RoleId == employeeRoleId)
                .Count();
        }

        public int GetUsersWithoutCompanyCountAsync()
        {
            var employeeRoleId = _unitOfWork.RoleRepository
                .Find(r => r.Name == "Employee")
                .Id;

            var userIdsWithoutCompany = _unitOfWork.UserRepository
                .FindAll(user => user.CompanyId== null)
                .Select(user => user.Id)
                .ToList();

            int count = _unitOfWork.UserRoleRepository
                .FindAll(ur => ur.RoleId == employeeRoleId && userIdsWithoutCompany.Contains(ur.UserId))
                .Count();

            return count;
        }

        public async Task<int> GetPostCountAsync()
        {
            return await _unitOfWork.PostRepository.CountAsync();
        }

        public async Task<IEnumerable<AuthDTO>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            return users.Select(user => new AuthDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                CompanyId = user.CompanyId,
                CompanyName = user.CompanyId!= null ? _unitOfWork.UserRepository.GetById(user.CompanyId).FullName : null,
                CompanyCode = user.CompanyId!= null ? _unitOfWork.UserRepository.GetById(user.CompanyId).CompanyCode : null,
                ProfileImage = _accountService.GetUserProfileImage(user.ProfileId).Result,
                CompanyProfile = user.CompanyId!= null ? _accountService.GetUserProfileImage(_unitOfWork.UserRepository.GetById(user.CompanyId).ProfileId).Result : null
            });
        }

        public async Task<IEnumerable<AuthDTO>> GetAllCompaniesAsync()
        {
            var companyRoleId = _unitOfWork.RoleRepository.Find(r => r.Name == "Company").Id;
            var companyIds = _unitOfWork.UserRoleRepository
                .FindAll(ur => ur.RoleId == companyRoleId)
                .Select(ur => ur.UserId)
                .ToList();

            var companies = await _unitOfWork.UserRepository.FindAllAsync(u => companyIds.Contains(u.Id));

            return companies.Select(company => new AuthDTO
            {
                Id = company.Id,
                FullName = company.FullName,
                Email = company.Email,
                CompanyId = company.Id,
                CompanyName = company.FullName,
                CompanyCode = company.CompanyCode,
                ProfileImage = _accountService.GetUserProfileImage(company.ProfileId).Result
            });
        }

        public async Task<IEnumerable<AuthDTO>> GetAllEmployeesAsync()
        {
            var employeeRoleId = _unitOfWork.RoleRepository.Find(r => r.Name == "Employee").Id;
            var employeeIds = _unitOfWork.UserRoleRepository
                .FindAll(ur => ur.RoleId == employeeRoleId)
                .Select(ur => ur.UserId)
                .ToList();

            var employees = await _unitOfWork.UserRepository.FindAllAsync(u => employeeIds.Contains(u.Id));

            return employees.Select(employee => new AuthDTO
            {
                Id = employee.Id,
                FullName = employee.FullName,
                Email = employee.Email,
                CompanyId = employee.CompanyId,
                CompanyName = employee.CompanyId!= null
                    ? null
                    : _unitOfWork.UserRepository.GetById(employee.CompanyId).FullName,
                ProfileImage = _accountService.GetUserProfileImage(employee.ProfileId).Result,
                CompanyCode = employee.CompanyId!= null ? _unitOfWork.UserRepository.GetById(employee.CompanyId).CompanyCode : null,
                CompanyProfile = employee.CompanyId!= null
                    ? null
                    : _accountService.GetUserProfileImage(
                          _unitOfWork.UserRepository.GetById(employee.CompanyId).ProfileId).Result
            });
        }

        public async Task<IEnumerable<PostDTO>> GetAllPostsAsync()
        {
            var posts = await _unitOfWork.PostRepository.GetAllAsync();

            return posts.Select(post => new PostDTO
            {
                Id = post.Id,
                Text = post.Text,
                Title = post.Title,
                Created = post.Date,
                Auth = post.Company != null ? new AuthDTO
                {
                    Id = post.Company.Id,
                    FullName = post.Company.FullName,
                    Email = post.Company.Email,
                    CompanyId = post.Company.Id,
                    CompanyName = post.Company.FullName,
                    ProfileImage = _accountService.GetUserProfileImage(post.Company.ProfileId).Result
                } : null,
                FileUrls = post.Files.Select(file => _fileService.GetFile(file.Id).Result).ToList()
            });
        }
    }
}
