using Kawkaba.BusinessLayer.Interfaces;
using Kawkaba.Core.DTO.AuthViewModel;
using Kawkaba.Core.Entity.ApplicationData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Kawkaba.Controllers.MVC
{
    [Authorize(Policy = "Admin")]
    public class UserController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(IAccountService accountService, UserManager<ApplicationUser> userManager)
        {
            _accountService = accountService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var userProfileImage = await _accountService.GetUserProfileImage(user.ProfileId);
            var userRole = await _accountService.GetUserRole(user);

            var model = new AuthDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                ProfileImage = userProfileImage,
                Role = userRole
            };

            return View(model);
        }

    }
}
