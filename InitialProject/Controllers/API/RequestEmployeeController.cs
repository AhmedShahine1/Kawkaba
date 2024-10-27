using Kawkaba.BusinessLayer.Interfaces;
using Kawkaba.BusinessLayer.Services;
using Kawkaba.Core.DTO;
using Kawkaba.Core.Entity.ApplicationData;
using Kawkaba.Core.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Kawkaba.Controllers.API
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RequestEmployeeController : BaseController, IActionFilter
    {
        private readonly ICompanyService companyService;
        private readonly IAccountService _accountService;
        private ApplicationUser? CurrentUser;

        public RequestEmployeeController(ICompanyService companyService, IAccountService accountService)
        {
            this.companyService = companyService;
            _accountService = accountService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Trim();

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    var user = _accountService.GetUserFromToken(token).Result;
                    CurrentUser = user; // Store the user in the context
                }
                catch (Exception)
                {
                    context.Result = new UnauthorizedResult(); // Early exit if user retrieval fails
                    return;
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        [HttpGet("AllRequest")]
        public async Task<IActionResult> AllRequest()
        {
            try
            {
                var result = await companyService.GetRequestCompany(CurrentUser.Id);
                return Ok(new BaseResponse
                {
                    status = true,
                    ErrorCode = 200,
                    ErrorMessage = string.Empty,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse
                {
                    status = false,
                    ErrorCode = 400,
                    ErrorMessage = "Get request failed",
                    Data = null
                });
            }
        }

        [HttpPost("employee/request")]
        public async Task<IActionResult> RequestToJoinCompany(int companyCode)
        {
            var employeeId = CurrentUser.Id;
            var result = await companyService.RequestToJoinCompany(employeeId, companyCode);

            if (result)
            {
                return Ok(new BaseResponse
                {
                    status = true,
                    ErrorCode = 200,
                    ErrorMessage = string.Empty,
                    Data = "Request sent to company"
                });
            }

            return BadRequest(new BaseResponse
            {
                status = false,
                ErrorCode = 400,
                ErrorMessage = "Request failed",
                Data = null
            });
        }

        [HttpPost("company/accept")]
        public async Task<IActionResult> AcceptEmployee(string requestId)
        {
            var result = await companyService.UpdateStatusRequest(requestId, StatusRequestEmployee.Accapted);

            if (result)
            {
                return Ok(new BaseResponse
                {
                    status = true,
                    ErrorCode = 200,
                    ErrorMessage = string.Empty,
                    Data = "Employee added successfully"
                });
            }

            return BadRequest(new BaseResponse
            {
                status = false,
                ErrorCode = 400,
                ErrorMessage = "Failed to add employee",
                Data = null
            });
        }

        [HttpPost("company/remove")]
        public async Task<IActionResult> RemoveEmployee(string requestId)
        {
            var result = await companyService.UpdateStatusRequest(requestId, StatusRequestEmployee.Canceled);

            if (result)
            {
                return Ok(new BaseResponse
                {
                    status = true,
                    ErrorCode = 200,
                    ErrorMessage = string.Empty,
                    Data = "Employee removed successfully"
                });
            }

            return BadRequest(new BaseResponse
            {
                status = false,
                ErrorCode = 400,
                ErrorMessage = "Failed to remove employee",
                Data = null
            });
        }

        [HttpPost("company/refuse")]
        public async Task<IActionResult> RefuseEmployeeRequest(string requestId)
        {
            var result = await companyService.UpdateStatusRequest(requestId, StatusRequestEmployee.Refused);

            if (result)
            {
                return Ok(new BaseResponse
                {
                    status = true,
                    ErrorCode = 200,
                    ErrorMessage = string.Empty,
                    Data = "Employee request refused"
                });
            }

            return BadRequest(new BaseResponse
            {
                status = false,
                ErrorCode = 400,
                ErrorMessage = "Failed to refuse employee request",
                Data = null
            });
        }


    }
}
