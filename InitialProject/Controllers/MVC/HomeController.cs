using Kawkaba.BusinessLayer.Interfaces;
using Kawkaba.BusinessLayer.Services;
using Kawkaba.Core.DTO.AuthViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kawkaba.Controllers.MVC
{
    [Authorize(Policy = "Admin")]
    public class HomeController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public HomeController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new DashboardViewModel
            {
                CompanyUserCount = _dashboardService.GetCompanyUserCountAsync(),
                EmployeeUserCount = _dashboardService.GetEmployeeUserCountAsync(),
                UsersWithoutCompanyCount = _dashboardService.GetUsersWithoutCompanyCountAsync(),
                PostCount = await _dashboardService.GetPostCountAsync()
            };

            return View(viewModel);
        }

        // Get all Users
        public async Task<IActionResult> Users()
        {
            var users = await _dashboardService.GetAllUsersAsync();
            return View(users);
        }

        // Get all Companies
        public async Task<IActionResult> Companies()
        {
            var companies = await _dashboardService.GetAllCompaniesAsync();
            return View(companies);
        }

        // Get all Employees
        public async Task<IActionResult> Employees()
        {
            var employees = await _dashboardService.GetAllEmployeesAsync();
            return View(employees);
        }

        // Get all Posts
        public async Task<IActionResult> Posts()
        {
            var posts = await _dashboardService.GetAllPostsAsync();
            return View(posts);
        }
    }
}
