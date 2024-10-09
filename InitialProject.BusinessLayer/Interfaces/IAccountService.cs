using Kawkaba.Core.DTO.AuthViewModel;
using Kawkaba.Core.DTO.AuthViewModel.RegisterModel;
using Kawkaba.Core.DTO.AuthViewModel.RoleModel;
using Kawkaba.Core.Entity.ApplicationData;
using Kawkaba.Core.Entity.Files;
using Microsoft.AspNetCore.Identity;

namespace Kawkaba.BusinessLayer.Interfaces;

public interface IAccountService
{
    Task<ApplicationUser> GetUserById(string id);
    Task<IdentityResult> RegisterAdmin(RegisterAdmin model);
    //Task<IdentityResult> UpdateAdmin(string adminId, RegisterAdmin model);
    Task<IdentityResult> RegisterSupportDeveloper(RegisterSupportDeveloper model);
    //Task<IdentityResult> UpdateSupportDeveloper(string SupportDeveloperId, RegisterSupportDeveloper model);
    Task<IdentityResult> RegisterEmployee(RegisterEmployee model);
    Task<IdentityResult> RegisterCompany(RegisterCompany model);
    Task<(bool IsSuccess, string Token, string ErrorMessage)> Login(LoginModel model);
    Task<bool> Logout(ApplicationUser user);
    Task<bool> ValidateOTP(string customerPhoneNumber, string OTPV);
    Task<ApplicationUser> GetUserFromToken(string token);
    Task<string> AddRoleAsync(RoleUserModel model);
    Task<List<string>> GetRoles();
    Task<string> GetUserProfileImage(string profileId);
    Task<Paths> GetPathByName(string name);
    string ValidateJwtToken(string token);
    int GenerateRandomNo();
    ////------------------------------------------------------
    Task<IdentityResult> Activate(string userId);
    Task<IdentityResult> Suspend(string userId);
}