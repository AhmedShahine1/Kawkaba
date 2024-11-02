using Kawkaba.BusinessLayer.Interfaces;
using Kawkaba.Core.DTO;
using Kawkaba.Core.DTO.AuthViewModel;
using Kawkaba.Core.DTO.AuthViewModel.RegisterModel;
using Kawkaba.Core.Entity.ApplicationData;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Kawkaba.Controllers.MVC
{
    public class AuthController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<ApplicationUser> userManager;

        public AuthController(IAccountService accountService,UserManager<ApplicationUser> userManager)
        {
            _accountService = accountService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            ViewData["Title"] = "Access Denied";
            return View();
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl = null)
        {
            ViewData["Title"] = "Login";
            ViewData["ReturnUrl"] = ReturnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string ReturnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var (isSuccess, token, errorMessage) = await _accountService.Login(model);

            if (isSuccess)
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = model.RememberMe ? DateTimeOffset.UtcNow.AddDays(30) : DateTimeOffset.UtcNow.AddHours(1),
                    Secure = true, // Use Secure cookie in production
                    SameSite = SameSiteMode.Strict
                };
                var user = await _accountService.GetUserFromToken(token);
                // Set the user ID in a cookie
                Response.Cookies.Append("UserProfile", await _accountService.GetUserProfileImage(user.ProfileId), cookieOptions);
                Response.Cookies.Append("UserName", user.FullName, cookieOptions);
                if (Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, errorMessage);
            return View(model);
        }

        [HttpGet]
        [HttpGet]
        public IActionResult RegisterAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAdmin(RegisterAdmin model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Please correct the errors and try again.");
                return View(model);
            }

            try
            {
                var result = await _accountService.RegisterAdmin(model);

                if (result.Succeeded)
                {
                    // Optionally, redirect to a success page or display a success message
                    TempData["SuccessMessage"] = "Registration successful!";
                    return RedirectToAction("Index", "Home"); // Adjust the redirection as needed
                }

                // If registration failed, display the errors
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            var user = await userManager.GetUserAsync(User);
            if (user != null)
            {
                var isLoggedOut = await _accountService.Logout(user);
                if (isLoggedOut)
                {
                    return RedirectToAction(nameof(Login));
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
