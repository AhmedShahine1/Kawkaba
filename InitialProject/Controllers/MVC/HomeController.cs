using Kawkaba.BusinessLayer.Services;
using Kawkaba.Core.DTO.AuthViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Kawkaba.Controllers.MVC
{
    public class HomeController : Controller
    {
        private readonly DashboardService _dashboardService;

        public HomeController(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new DashboardViewModel
            {
                CompanyUserCount = await _dashboardService.GetCompanyUserCountAsync(),
                EmployeeUserCount = await _dashboardService.GetEmployeeUserCountAsync(),
                UsersWithoutCompanyCount = await _dashboardService.GetUsersWithoutCompanyCountAsync(),
                PostCount = await _dashboardService.GetPostCountAsync()
            };

            return View(viewModel);
        }
    }
}
