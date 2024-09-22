using InitialProject.BusinessLayer.Interfaces;

namespace InitialProject.BusinessLayer.Services;

public class AccountService : IAccountService
{
    //private readonly UserManager<ApplicationUser> _userManager;
    //private readonly RoleManager<ApplicationRole> _roleManager;
    //private readonly IUnitOfWork _unitOfWork;
    //private readonly IFileHandling _fileHandling;
    //private readonly Jwt _jwt;
    //private readonly IHttpClientFactory _clientFactory;

    //public AccountService(IHttpClientFactory clientFactory,UserManager<ApplicationUser> userManager, IFileHandling photoHandling,
    //    RoleManager<ApplicationRole> roleManager, IUnitOfWork unitOfWork,
    //    IOptions<Jwt> jwt)
    //{
    //    _clientFactory = clientFactory;
    //    _userManager = userManager;
    //    _roleManager = roleManager;
    //    _unitOfWork = unitOfWork;
    //    _jwt = jwt.Value;
    //    _fileHandling = photoHandling;
    //}

    //public async Task<List<ApplicationUser>> GetAllUsers()
    //{
    //    return await _userManager.Users.ToListAsync();
    //}

    //public async Task<ApplicationUser> GetUserById(string userId)
    //{
    //    var user = await _userManager.FindByIdAsync(userId);
    //    if (user is null)
    //        return null;
    //    return user;
    //}

    //public async Task<ApplicationUser> GetUserByPhoneNumber(string phoneNumber)
    //{
    //    var user = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
    //    return user ;
    //}

    //public async Task<ApplicationUser> GetUserByEmail(string email)
    //{
    //    var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
    //    return user ;
    //}

    //// ------------------------------------------------------------------------------------------------------------------
    //public async Task<AuthModel> RegisterInitialProject(RegisterInitialProject model)
    //{
    //    if (await _userManager.FindByEmailAsync(model.Email) is not null)
    //        return new AuthModel { Message = "this email is already Exist!", ArMessage = "هذا البريد الالكتروني مستخدم من قبل", ErrorCode = (int)Errors.ThisEmailAlreadyExist };

    //    if (await Task.Run(() => _userManager.Users.Any(item => item.PhoneNumber == model.PhoneNumber)))
    //        return new AuthModel { Message = "this phone number is already Exist!", ArMessage = "هذا الرقم المحمول مستخدم من قبل", ErrorCode = (int)Errors.ThisPhoneNumberAlreadyExist };

    //    string imgUrl;
    //    try
    //    {
    //        if (model.Img!=null)
    //            imgUrl = await _fileHandling.UploadFile(model.Img, "InitialProjectImg");
    //        else
    //            return new AuthModel { Message = "please select  Img for Account!", ArMessage = " من فضلك حدد صورة شخصية ", ErrorCode = (int)Errors.NoPhoto };
    //    }
    //    catch
    //    {
    //        return new AuthModel { Message = "error in uploading Img!", ArMessage = "حدث خطأ في رفع الصورة", ErrorCode = (int)Errors.ErrorInUploadingImg };
    //    }

    //    var user = new ApplicationUser
    //    {
    //        FirstName = model.FirstName,
    //        LastName = model.LastName,
    //        UserName = model.Email,
    //        NormalizedUserName = model.Email.ToUpper(),
    //        PhoneNumber = model.PhoneNumber,
    //        Email = model.Email,
    //        UserImgUrl = imgUrl,
    //        Age=model.age,
    //        Qualification = model.Qualification,
    //        Status = true,
    //        PhoneNumberConfirmed = true,
    //        UserType = UserType.InitialProject,
    //        IsAdmin = false,
    //        Job = model.Job
    //    };
    //    var result = await _userManager.CreateAsync(user, model.Password);
    //    if (!result.Succeeded)
    //    {
    //        var errors = result.Errors.Aggregate(string.Empty, (current, error) => current + $"{error.Description},");
    //        return new AuthModel { Message = errors, ArMessage = errors, ErrorCode = (int)Errors.ErrorWithCreateAccount };
    //    }
    //    await _userManager.AddToRoleAsync(user, "InitialProject");

    //    {
    //        var InitialProject = await _userManager.FindByNameAsync(model.Email);

    //        if (InitialProject is null)
    //            return new AuthModel
    //            {
    //                Message = "An error occurred creating the account!",
    //                ArMessage = "حدث خطأ أثناء إنشاء الحساب!",
    //                ErrorCode = (int)Errors.ErrorWithCreateAccount
    //            };
    //        return new AuthModel
    //        {
    //            Email = InitialProject.Email,
    //            PhoneNumber = InitialProject.PhoneNumber,
    //            FirstName = InitialProject.FirstName,
    //            LastName = InitialProject.LastName,
    //            IsAuthenticated = true,
    //            Qualification = InitialProject.Qualification,
    //            Roles = new List<string> { "InitialProject" },
    //            UserType = UserType.InitialProject,
    //            UserImgUrl = InitialProject.UserImgUrl,
    //            Job = InitialProject.Job,
    //            ArMessage = "تم انشاء الحساب بنجاح",
    //            Message = "Account successfully created",
    //        };
    //    }
    //}

    //public async Task<AuthModel> RegisterYouth(RegisterYouth model)
    //{
    //    if (await _userManager.FindByEmailAsync(model.Email) is not null)
    //        return new AuthModel { Message = "this email is already Exist!", ArMessage = "هذا البريد الالكتروني مستخدم من قبل", ErrorCode = (int)Errors.ThisEmailAlreadyExist };

    //    if (await Task.Run(() => _userManager.Users.Any(item => item.PhoneNumber == model.PhoneNumber)))
    //        return new AuthModel { Message = "this phone number is already Exist!", ArMessage = "هذا الرقم المحمول مستخدم من قبل", ErrorCode = (int)Errors.ThisPhoneNumberAlreadyExist };

    //    string imgUrl;
    //    try
    //    {
    //        if (model.Img != null)
    //            imgUrl = await _fileHandling.UploadFile(model.Img, "YouthImg");
    //        else
    //            return new AuthModel { Message = "please select  Img for Account!", ArMessage = " من فضلك حدد صورة شخصية ", ErrorCode = (int)Errors.NoPhoto };
    //    }
    //    catch
    //    {
    //        return new AuthModel { Message = "error in uploading Img!", ArMessage = "حدث خطأ في رفع الصورة", ErrorCode = (int)Errors.ErrorInUploadingImg };
    //    }

    //    var user = new ApplicationUser
    //    {
    //        FirstName = model.FirstName,
    //        LastName = model.LastName,
    //        UserName = model.Email,
    //        NormalizedUserName = model.Email.ToUpper(),
    //        PhoneNumber = model.PhoneNumber,
    //        Email = model.Email,
    //        Age = model.age,
    //        UserImgUrl = imgUrl,
    //        Qualification = model.Qualification,
    //        Status = true,
    //        PhoneNumberConfirmed = true,
    //        UserType = UserType.Youth,
    //        IsAdmin = false,
    //        Job = model.Job
    //    };
    //    var result = await _userManager.CreateAsync(user, model.Password);
    //    if (!result.Succeeded)
    //    {
    //        var errors = result.Errors.Aggregate(string.Empty, (current, error) => current + $"{error.Description},");
    //        return new AuthModel { Message = errors, ArMessage = errors, ErrorCode = (int)Errors.ErrorWithCreateAccount };
    //    }
    //    await _userManager.AddToRoleAsync(user, "Youth");

    //    {
    //        var Youth = await _userManager.FindByNameAsync(model.Email);

    //        if (Youth is null)
    //            return new AuthModel
    //            {
    //                Message = "An error occurred creating the account!",
    //                ArMessage = "حدث خطأ أثناء إنشاء الحساب!",
    //                ErrorCode = (int)Errors.ErrorWithCreateAccount
    //            };
    //        return new AuthModel
    //        {
    //            Email = Youth.Email,
    //            PhoneNumber = Youth.PhoneNumber,
    //            FirstName = Youth.FirstName,
    //            LastName = Youth.LastName,
    //            IsAuthenticated = true,
    //            Qualification = Youth.Qualification,
    //            Roles = new List<string> { "Youth" },
    //            UserType = UserType.Youth,
    //            UserImgUrl = Youth.UserImgUrl,
    //            Job = Youth.Job,
    //            ArMessage = "تم انشاء الحساب بنجاح",
    //            Message = "Account successfully created",
    //        };
    //    }
    //}

    //public async Task<AuthModel> RegisterAdmin(RegisterAdmin model)
    //{
    //    if (await _userManager.FindByEmailAsync(model.Email) is not null)
    //        return new AuthModel { Message = "this email is already Exist!", ArMessage = "هذا البريد الالكتروني مستخدم من قبل", ErrorCode = (int)Errors.ThisEmailAlreadyExist };

    //    if (await Task.Run(() => _userManager.Users.Any(item => item.PhoneNumber == model.PhoneNumber)))
    //        return new AuthModel { Message = "this phone number is already Exist!", ArMessage = "هذا الرقم المحمول مستخدم من قبل", ErrorCode = (int)Errors.ThisPhoneNumberAlreadyExist };

    //    string imgUrl;
    //    try
    //    {
    //        if (model.Img != null)
    //            imgUrl = await _fileHandling.UploadFile(model.Img, "AdminImg");
    //        else
    //            return new AuthModel { Message = "please select  Img for Account!", ArMessage = " من فضلك حدد صورة شخصية  ", ErrorCode = (int)Errors.NoPhoto };
    //    }
    //    catch
    //    {
    //        return new AuthModel { Message = "error in uploading Img!", ArMessage = "حدث خطأ في رفع الصورة", ErrorCode = (int)Errors.ErrorInUploadingImg };
    //    }

    //    var user = new ApplicationUser
    //    {
    //        FirstName = model.FirstName,
    //        LastName = model.LastName,
    //        UserName = model.Email,
    //        NormalizedUserName = model.Email.ToUpper(),
    //        PhoneNumber = model.PhoneNumber,
    //        Email = model.Email,
    //        UserImgUrl = imgUrl,
    //        Age = model.age,
    //        Qualification = model.Qualification,
    //        Status = true,
    //        PhoneNumberConfirmed = true,
    //        UserType = UserType.Admin,
    //        IsAdmin = true,
    //        Job = model.Job
    //    };
    //    var result = await _userManager.CreateAsync(user, model.Password);
    //    if (!result.Succeeded)
    //    {
    //        var errors = result.Errors.Aggregate(string.Empty, (current, error) => current + $"{error.Description},");
    //        return new AuthModel { Message = errors, ArMessage = errors, ErrorCode = (int)Errors.ErrorWithCreateAccount };
    //    }
    //    await _userManager.AddToRoleAsync(user, "Admin");

    //    {
    //        var Admin = await _userManager.FindByNameAsync(model.Email);

    //        if (Admin is null)
    //            return new AuthModel
    //            {
    //                Message = "An error occurred creating the account!",
    //                ArMessage = "حدث خطأ أثناء إنشاء الحساب!",
    //                ErrorCode = (int)Errors.ErrorWithCreateAccount
    //            };
    //        return new AuthModel
    //        {
    //            Email = Admin.Email,
    //            PhoneNumber = Admin.PhoneNumber,
    //            FirstName = Admin.FirstName,
    //            LastName = Admin.LastName,
    //            IsAuthenticated = true,
    //            Qualification = Admin.Qualification,
    //            Roles = new List<string> { "Admin" },
    //            UserType = UserType.Admin,
    //            UserImgUrl = Admin.UserImgUrl,
    //            Job = Admin.Job,
    //            ArMessage = "تم انشاء الحساب بنجاح",
    //            Message = "Account successfully created",
    //        };
    //    }
    //}

    ////-------------------------------------------------------------------------------------------------------------------------
    //public async Task<AuthModel> LoginAsync(LoginModel model)
    //{
    //    var user = await _userManager.FindByNameAsync(model.Email);
    //    if (user is null)
    //        return new AuthModel { Message = "Your phone number is not Exist!", ArMessage = "الايميل غير مسجل", ErrorCode = (int)Errors.ThisPhoneNumberNotExist };
    //    if (!await _userManager.CheckPasswordAsync(user, model.Password))
    //        return new AuthModel { Message = "Password is not correct!", ArMessage = "كلمة المرور غير صحيحة", ErrorCode = (int)Errors.TheUsernameOrPasswordIsIncorrect };
    //    if (!user.Status)
    //        return new AuthModel { Message = "Your account has been suspended!", ArMessage = "حسابك تم إيقافة", ErrorCode = (int)Errors.UserIsBlocked };

    //    var rolesList = _userManager.GetRolesAsync(user).Result.ToList();
    //    return new AuthModel
    //    {
    //        UserId = user.Id,
    //        Email = user.Email,
    //        PhoneNumber = user.PhoneNumber,
    //        FirstName = user.FirstName,
    //        LastName = user.LastName,
    //        IsAuthenticated = true,
    //        Roles = rolesList,
    //        UserType = user.UserType,
    //        age = user.Age,
    //        IsAdmin = user.IsAdmin,
    //        Token = new JwtSecurityTokenHandler().WriteToken(GenerateJwtToken(user).Result),
    //        UserImgUrl = user.UserImgUrl,
    //        Qualification = user.Qualification,
    //        Job = user.Job,
    //        Message = "Login successfully",
    //        ArMessage = "تم تسجيل الدخول بنجاح"
    //    };
    //}

    //public async Task<bool> Logout(string userName)
    //{
    //    var user = await _userManager.FindByNameAsync(userName);
    //    if (user is null)
    //        return false;
    //    await _userManager.UpdateAsync(user);
    //    return true;
    //}

    ////-------------------------------------------------------------------------------------------------------------------------
    //public async Task<AuthModel> ChangePasswordAsync(string userId, string password)
    //{
    //    var user = await _userManager.FindByIdAsync(userId);
    //    if (user is null)
    //        return new AuthModel { Message = "User not found!", ArMessage = "المستخدم غير موجود", ErrorCode = (int)Errors.TheUserNotExistOrDeleted };

    //    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password);
    //    await _userManager.UpdateAsync(user);

    //    var jwtSecurityToken = await GenerateJwtToken(user, 1);
    //    var rolesList = await _userManager.GetRolesAsync(user);

    //    var result = new AuthModel
    //    {
    //        Message = "The password has been changed successfully",
    //        ArMessage = "تم تغيير كلمة المرور بنجاح",
    //        Email = user.Email,
    //        PhoneNumber = user.PhoneNumber,
    //        FirstName = user.FirstName,
    //        LastName = user.LastName,
    //        UserImgUrl = user.UserImgUrl,
    //        IsAuthenticated = true,
    //        Roles = rolesList.ToList(),
    //        UserType = user.UserType,
    //        IsAdmin = user.IsAdmin,
    //        Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
    //    };
    //    return result;
    //}

    //public async Task<AuthModel> ChangeOldPasswordAsync(string userId, ChangeOldPassword changePassword)
    //{
    //    var user = await _userManager.FindByIdAsync(userId);
    //    if (user is null)
    //        return new AuthModel { Message = "User not found!", ArMessage = "المستخدم غير موجود", ErrorCode = (int)Errors.TheUserNotExistOrDeleted };

    //    if (user.PasswordHash != null)
    //    {
    //        var isOldCorrect = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, changePassword.OldPassword);
    //        if (!isOldCorrect.Equals(PasswordVerificationResult.Success))
    //            return new AuthModel { Message = "Old password is incorrect!", ArMessage = "كلمة المرور القديمة غير صحيحة", ErrorCode = (int)Errors.OldPasswordIsIncorrect };
    //    }
    //    else
    //    {
    //        return new AuthModel { Message = "Old password is not Exist!", ArMessage = "كلمة المرور القديمة غير موجودة", ErrorCode = (int)Errors.OldPasswordIsExist };
    //    }

    //    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, changePassword.NewPassword);
    //    await _userManager.UpdateAsync(user);

    //    var jwtSecurityToken = await GenerateJwtToken(user, 1);
    //    var rolesList = await _userManager.GetRolesAsync(user);

    //    var result = new AuthModel
    //    {
    //        Message = "The password has been changed successfully",
    //        ArMessage = "تم تغيير كلمة المرور بنجاح",
    //        Email = user.Email,
    //        PhoneNumber = user.PhoneNumber,
    //        UserImgUrl = user.UserImgUrl,
    //        FirstName = user.FirstName,
    //        LastName = user.LastName,
    //        IsAuthenticated = true,
    //        Roles = rolesList.ToList(),
    //        UserType = user.UserType,
    //        IsAdmin = user.IsAdmin,
    //        Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
    //    };
    //    return result;
    //}

    ////-------------------------------------------------------------------------------------------------------------------------

    //public async Task<AuthModel> UpdateUserProfile(string userId, UpdateUserMv model)
    //{
    //    var user = await _userManager.FindByIdAsync(userId);
    //    if (user is null)
    //        return new AuthModel {ErrorCode = (int)Errors.TheUserNotExistOrDeleted,Message = "User not found!", ArMessage = "المستخدم غير موجود" };
    //    if (await Task.Run(() => _userManager.Users.Any(item => (item.PhoneNumber == model.PhoneNumber) && (item.Id != userId))))
    //        return new AuthModel { Message = "this phone number is already Exist!", ArMessage = "هذا الرقم المحمول مستخدم من قبل" };
    //    if (await Task.Run(() => _userManager.Users.Any(item => (item.Email == model.Email) && (item.Id != userId))))
    //        return new AuthModel { Message = "this email is already Exist!", ArMessage = "هذا البريد الالكتروني مستخدم من قبل" };
    //    string imgUrl = null;
    //    try
    //    {
    //        if (model.Img != null)
    //        {
    //            switch (user.UserType)
    //            {
    //                case UserType.Admin: imgUrl = await _fileHandling.UploadFile(model.Img, "AdminImg", user.UserImgUrl);
    //                    break;
    //                case UserType.Youth: imgUrl = await _fileHandling.UploadFile(model.Img, "YouthImg", user.UserImgUrl);
    //                    break;
    //                case UserType.InitialProject: imgUrl = await _fileHandling.UploadFile(model.Img, "InitialProjectImg", user.UserImgUrl);
    //                    break;
    //            }
    //        }
    //    }
    //    catch
    //    {
    //        return new AuthModel { Message = "error in uploading Img!", ArMessage = "حدث خطأ في رفع الصورة", ErrorCode = (int)Errors.ErrorInUploadingImg };
    //    }
    //    user.Email = model.Email;
    //    user.NormalizedEmail = model.Email;
    //    user.NormalizedUserName = model.Email;
    //    user.UserName = model.Email;
    //    user.PhoneNumber = model.PhoneNumber;
    //    user.FirstName = model.FirstName;
    //    user.LastName = model.LastName;
    //    user.Qualification = model.Qualification;
    //    user.UserImgUrl = (!string.IsNullOrEmpty(imgUrl)) ? imgUrl : user.UserImgUrl;
    //    user.Job = model.Job;
    //    user.Age = model.age;
    //    await _userManager.UpdateAsync(user);

    //    var jwtSecurityToken = await GenerateJwtToken(user);
    //    var rolesList = await _userManager.GetRolesAsync(user);

    //    var result = new AuthModel
    //    {
    //        Message = "The profile has been updated successfully",
    //        ArMessage = "تم تحديث الملف الشخصي بنجاح",
    //        Email = user.Email,
    //        PhoneNumber = user.PhoneNumber,
    //        FirstName = user.FirstName,
    //        LastName = user.LastName,
    //        IsAuthenticated = true,
    //        Qualification = user.Qualification,
    //        Roles = rolesList.ToList(),
    //        UserType = user.UserType,
    //        UserImgUrl = user.UserImgUrl,
    //        Job = user.Job,
    //        Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
    //    };
    //    return result;
    //}

    //public async Task<AuthModel> GetUserInfo(string userId)
    //{
    //    var user = await _userManager.FindByIdAsync(userId);
    //    if (user is null)
    //        return new AuthModel {ErrorCode = (int)Errors.TheUserNotExistOrDeleted,Message = "User not found!", ArMessage = "المستخدم غير موجود" };

    //    var rolesList = await _userManager.GetRolesAsync(user);
    //    var result = new AuthModel
    //    {
    //        Email = user.Email,
    //        PhoneNumber = user.PhoneNumber,
    //        FirstName = user.FirstName,
    //        LastName = user.LastName,
    //        IsAuthenticated = true,
    //        Qualification = user.Qualification,
    //        UserType = user.UserType,
    //        UserImgUrl = user.UserImgUrl,
    //        Job = user.Job,
    //        Roles = rolesList.ToList(),
    //        Status = user.Status,
    //    };
    //    return result;
    //}

    ////------------------------------------------------------------------------------------------------------------
    //public async Task<string> AddRoleAsync(AddRoleModel model)
    //{
    //    var user = await _userManager.FindByIdAsync(model.UserId);
    //    if (user is null)
    //        return "User not found!";

    //    if (model.Roles != null && model.Roles.Count > 0)
    //    {
    //        foreach (var role in model.Roles)
    //        {
    //            if (!await _roleManager.RoleExistsAsync(role))
    //                return "Invalid Role";
    //            if (await _userManager.IsInRoleAsync(user, role))
    //                return "User already assigned to this role";
    //        }
    //        var result = await _userManager.AddToRolesAsync(user, model.Roles);

    //        return result.Succeeded ? string.Empty : "Something went wrong";
    //    }
    //    return " Role is empty";
    //}

    //public Task<List<string>> GetRoles()
    //{
    //    return _roleManager.Roles.Select(x => x.Name).ToListAsync();
    //}

    ////------------------------------------------------------------------------------------------------------------

    //public async Task Activate(string userId)
    //{
    //    var user = await _userManager.FindByIdAsync(userId);
    //    if (user != null)
    //    {
    //        user.Status = true;
    //        await _userManager.UpdateAsync(user);
    //    }
    //}

    //public async Task Suspend(string userId)
    //{
    //    var user = await _userManager.FindByIdAsync(userId);
    //    if (user != null)
    //    {
    //        user.Status = false;
    //        await _userManager.UpdateAsync(user);
    //    }
    //}

    ////------------------------------------------------------------------------------------------------------------

    //#region create and validate JWT token

    //private async Task<JwtSecurityToken> GenerateJwtToken(ApplicationUser user, int? time = null)
    //{
    //    var userClaims = await _userManager.GetClaimsAsync(user);
    //    var roles = await _userManager.GetRolesAsync(user);
    //    var roleClaims = roles.Select(role => new Claim("roles", role)).ToList();
    //    var userType = user.UserType.ToString();

    //    var claims = new[]
    //        {
    //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    //            new Claim("uid", user.Id),
    //            new Claim("Name", user.Email),
    //            new Claim("userType",userType),
    //            (user.IsAdmin) ? new Claim("isAdmin", "true") : new Claim("isAdmin", "false"),
    //        }
    //        .Union(userClaims)
    //        .Union(roleClaims);

    //    var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
    //    var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

    //    var jwtSecurityToken = new JwtSecurityToken(
    //        issuer: _jwt.Issuer,
    //        audience: _jwt.Audience,
    //        claims: claims,
    //        expires: (time != null) ? DateTime.Now.AddHours((double)time) : DateTime.Now.AddDays(_jwt.DurationInDays),
    //        signingCredentials: signingCredentials);

    //    return jwtSecurityToken;
    //}


    //public string ValidateJwtToken(string token)
    //{
    //    var tokenHandler = new JwtSecurityTokenHandler();
    //    try
    //    {
    //        if (token == null)
    //            return null;
    //        if (token.StartsWith("Bearer "))
    //            token = token.Replace("Bearer ", "");

    //        tokenHandler.ValidateToken(token, new TokenValidationParameters
    //        {
    //            ValidateIssuerSigningKey = true,
    //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key)),
    //            ValidateIssuer = false,
    //            ValidateAudience = false,
    //            ClockSkew = TimeSpan.Zero
    //        }, out var validatedToken);

    //        var jwtToken = (JwtSecurityToken)validatedToken;
    //        var accountId = jwtToken.Claims.First(x => x.Type == "uid").Value;

    //        return accountId;
    //    }
    //    catch
    //    {
    //        return null;
    //    }
    //}

    //#endregion create and validate JWT token

    //#region Random number and string

    ////Generate RandomNo
    //public int GenerateRandomNo()
    //{
    //    const int min = 1000;
    //    const int max = 9999;
    //    var rdm = new Random();
    //    return rdm.Next(min, max);
    //}


    //public  string RandomString(int length)
    //{
    //    var random = new Random();
    //    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    //    return new string(Enumerable.Repeat(chars, length)
    //        .Select(s => s[random.Next(s.Length)]).ToArray());
    //}

    //#endregion Random number and string

    ////----------------------------------------------------------------------------------------------------- Function Connection
    //public async Task<bool> ActiveUserConnnection(string userId)
    //{
    //    if (string.IsNullOrEmpty(userId))
    //    {
    //        return false;
    //    }
    //    var UserConnection = new UserConnection
    //    {
    //        UserId = userId,
    //    };
    //    try
    //    {
    //        await _unitOfWork.UserConnections.AddAsync(UserConnection);
    //        await _unitOfWork.SaveChangesAsync();
    //    }
    //    catch
    //    {
    //        return false;
    //    }

    //    return true;
    //}

    //public async Task<bool> DisActiveUserConnnection(string userId)
    //{
    //    if (string.IsNullOrEmpty(userId))
    //    {
    //        return false;
    //    }
    //    var UserConnection = new UserConnection
    //    {
    //        UserId = userId,
    //        Connection = false
    //    };
    //    try
    //    {
    //        await _unitOfWork.UserConnections.AddAsync(UserConnection);
    //        await _unitOfWork.SaveChangesAsync();
    //    }
    //    catch
    //    {
    //        return false;
    //    }

    //    return true;
    //}
}