using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Kawkaba.BusinessLayer.Interfaces;
using Kawkaba.Core.DTO;
using Kawkaba.Core.DTO.AuthViewModel;
using Kawkaba.Core.DTO.AuthViewModel.RegisterModel;
using Kawkaba.BusinessLayer.Services;
using Kawkaba.Core.Helpers;
using Microsoft.AspNetCore.Components.Forms.Mapping;

namespace Kawkaba.Controllers.API
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ICompanyService _companyService;

        public AccountController(IAccountService accountService, IMapper mapper, IEmailService emailService, ICompanyService companyService)
        {
            _accountService = accountService;
            _mapper = mapper;
            _emailService = emailService;
            _companyService = companyService;
        }

        [HttpGet("User")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCustomerDetails()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Trim();
                var customer = await _accountService.GetUserFromToken(token);
                if (customer == null)
                {
                    return NotFound(new BaseResponse
                    {
                        status = false,
                        ErrorCode = 404,
                        ErrorMessage = "Customer not found"
                    });
                }

                var authDto = _mapper.Map<AuthDTO>(customer);
                authDto.ProfileImage = await _accountService.GetUserProfileImage(customer.ProfileId);
                authDto.Role = await _accountService.GetUserRole(customer);
                authDto.Employees = (authDto.Role == "Company") ? _mapper.Map<List<AuthDTO>>(await _companyService.GetCompanyEmployees(customer.Id)) : null;

                return Ok(new BaseResponse
                {
                    status = true,
                    Data = authDto
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new BaseResponse
                {
                    status = false,
                    ErrorCode = 500,
                    ErrorMessage = "An unexpected error occurred.",
                    Data = ex.Message
                });
            }
        }

        [HttpPost("RegisterEmployee")]
        public async Task<IActionResult> RegisterEmployee([FromBody] RegisterEmployee model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse
                {
                    status = false,
                    ErrorCode = 400,
                    ErrorMessage = "Invalid model"
                });
            }

            try
            {
                var result = await _accountService.RegisterEmployee(model);

                if (result.Succeeded)
                {
                    return Ok(new BaseResponse
                    {
                        status = true,
                        ErrorCode = 200,
                        Data = model // Adjust if necessary
                    });
                }

                return BadRequest(new BaseResponse
                {
                    status = false,
                    ErrorCode = 500,
                    ErrorMessage = "User registration failed.",
                    Data = result.Errors.Select(e => e.Description).ToArray()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new BaseResponse
                {
                    status = false,
                    ErrorCode = 500,
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpPost("RegisterCompany")]
        public async Task<IActionResult> RegisterCompany([FromBody] RegisterCompany model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse
                {
                    status = false,
                    ErrorCode = 400,
                    ErrorMessage = "Invalid model"
                });
            }

            try
            {
                var result = await _accountService.RegisterCompany(model);

                if (result.Succeeded)
                {
                    return Ok(new BaseResponse
                    {
                        status = true,
                        ErrorCode = 200,
                        Data = model // Adjust if necessary
                    });
                }

                return BadRequest(new BaseResponse
                {
                    status = false,
                    ErrorCode = 500,
                    ErrorMessage = "User registration failed.",
                    Data = result.Errors.Select(e => e.Description).ToArray()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new BaseResponse
                {
                    status = false,
                    ErrorCode = 500,
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse
                {
                    status = false,
                    ErrorCode = 400,
                    ErrorMessage = "Invalid model"
                });
            }

            var result = await _accountService.Login(model);

            if (result.IsSuccess)
            {
                var user = await _accountService.GetUserFromToken(result.Token);
                var authDto = _mapper.Map<AuthDTO>(user);
                authDto.Token = result.Token;
                authDto.ProfileImage = await _accountService.GetUserProfileImage(user.ProfileId);
                authDto.Role = await _accountService.GetUserRole(user);
                authDto.Employees = (authDto.Role == "Company") ? _mapper.Map<List<AuthDTO>>(await _companyService.GetCompanyEmployees(user.Id)) : null;
                foreach (var employee in authDto.Employees)
                {
                    employee.ProfileImage = await _accountService.GetUserProfileImage(employee.ProfileImageId);
                }
                return Ok(new BaseResponse
                {
                    status = true,
                    ErrorCode = 200,
                    Data = authDto
                });
            }
            if (result.Token == "405")
            {
                return BadRequest(new BaseResponse
                {
                    status = false,
                    ErrorCode = 405,
                    ErrorMessage = result.ErrorMessage
                });

            }
            return Unauthorized(new BaseResponse
            {
                status = false,
                ErrorCode = 401,
                ErrorMessage = result.ErrorMessage
            });
        }

        [HttpPost("logout")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var user = await _accountService.GetUserFromToken(token);
                var isSuccess = await _accountService.Logout(user);

                if (isSuccess)
                {
                    return Ok(new BaseResponse
                    {
                        status = true,
                        Data = "Successfully logged out"
                    });
                }

                return BadRequest(new BaseResponse
                {
                    status = false,
                    ErrorCode = 400,
                    ErrorMessage = "Logout failed"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new BaseResponse
                {
                    status = false,
                    ErrorCode = 500,
                    ErrorMessage = "An unexpected error occurred.",
                    Data = ex.Message
                });
            }
        }

        // POST: api/email/sendotp
        [HttpPost("SendOTP")]
        public async Task<IActionResult> SendOtp([FromBody] EmailRequest emailRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // This will return any validation errors, like an invalid email format
            }

            try
            {
                // Generate OTP (for simplicity, using a 6-digit random number)
                var otpCode = new Random().Next(100000, 999999).ToString();

                // Prepare the email content
                var subject = "Your OTP Code";
                var body = $"Your OTP code is {otpCode}. It will expire in 5 minutes.";

                // Send the OTP email
                var result = await _emailService.SendEmailAsync(emailRequest.ToEmail, subject, body);
                if (result)
                    // Return success response
                    return Ok(new { Message = "OTP sent successfully", OtpCode = otpCode });
                else
                    return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while sending OTP: {ex.Message}");
            }
        }

        [HttpPost("ValidateOTP/{Email}/{OTP}")]
        public async Task<IActionResult> ConfirmPhoneNumber(string Email, string OTP)
        {
            if (string.IsNullOrEmpty(Email))
            {
                return BadRequest(new BaseResponse
                {
                    status = false,
                    ErrorCode = 400,
                    ErrorMessage = "Customer Phone Number cannot be null or empty"
                });
            }

            try
            {
                //var result = await _accountService.ValidateOTP(customerPhoneNumber,OTP);

                if (true)
                {
                    return Ok(new BaseResponse
                    {
                        status = true,
                        Data = new { Message = "Email confirmed successfully." }
                    });
                }

                return BadRequest(new BaseResponse
                {
                    status = false,
                    ErrorCode = 400,
                    ErrorMessage = "Failed to confirm Email.",
                });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new BaseResponse
                {
                    status = false,
                    ErrorCode = 404,
                    ErrorMessage = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new BaseResponse
                {
                    status = false,
                    ErrorCode = 500,
                    ErrorMessage = "An unexpected error occurred.",
                    Data = ex.Message
                });
            }
        }

        [HttpPost("Delete")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteAccount()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var customer = await _accountService.GetUserFromToken(token);

                if (customer == null)
                {
                    return NotFound(new BaseResponse
                    {
                        status = false,
                        ErrorCode = 404,
                        ErrorMessage = "Customer not found"
                    });
                }

                var result = await _accountService.Suspend(customer.Id);

                if (result.Succeeded)
                {
                    return Ok(new BaseResponse
                    {
                        status = true,
                        Data = "User account deleted successfully."
                    });
                }

                return BadRequest(new BaseResponse
                {
                    status = false,
                    ErrorCode = 400,
                    ErrorMessage = "Failed to delete account",
                    Data = result.Errors.Select(e => e.Description).ToArray()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new BaseResponse
                {
                    status = false,
                    ErrorCode = 500,
                    ErrorMessage = "An unexpected error occurred.",
                    Data = ex.Message
                });
            }
        }

        [HttpGet("Employees")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Company")]
        public async Task<IActionResult> Employees()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var Company = await _accountService.GetUserFromToken(token);

                if (Company == null)
                {
                    return NotFound(new BaseResponse
                    {
                        status = false,
                        ErrorCode = 404,
                        ErrorMessage = "Customer not found"
                    });
                }
                var Employees = _mapper.Map<List<AuthDTO>>(await _companyService.GetCompanyEmployees(Company.Id));
                foreach (var employee in Employees)
                {
                    employee.ProfileImage = await _accountService.GetUserProfileImage(employee.ProfileImageId);
                }
                return Ok(new BaseResponse
                {
                    status = true,
                    ErrorCode = 200,
                    Data = Employees
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new BaseResponse
                {
                    status = false,
                    ErrorCode = 500,
                    ErrorMessage = "An unexpected error occurred.",
                    Data = ex.Message
                });
            }
        }

    }
}