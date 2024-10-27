using AutoMapper;
using Kawkaba.BusinessLayer.Interfaces;
using Kawkaba.Core.DTO.AuthViewModel.PostsModel;
using Kawkaba.Core.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Kawkaba.BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Kawkaba.Core.Entity.ApplicationData;

namespace Kawkaba.Controllers.API
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostController : BaseController , IActionFilter
    {
        private readonly IPostService _postService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private ApplicationUser? CurrentUser;

        public PostController(IPostService postService, IAccountService accountService, IMapper mapper)
        {
            _postService = postService;
            _accountService = accountService;
            _mapper = mapper;
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

        [HttpPost("Add")]
        public async Task<IActionResult> CreatePost([FromForm] PostDTO postDto)
        {
            string companyId = CurrentUser.Id;

            if (string.IsNullOrEmpty(companyId))
            {
                return BadRequest(new BaseResponse
                {
                    status = false,
                    ErrorCode = 400,
                    ErrorMessage = "Invalid company ID"
                });
            }

            var postResponse = await _postService.CreatePostAsync(postDto, companyId);

            return Ok(new BaseResponse
            {
                status = true,
                ErrorCode = 0,
                ErrorMessage = string.Empty,
                Data = postResponse
            });
        }

        [HttpGet("Employee/posts")]
        public async Task<IActionResult> GetEmployeePosts(string CompanyId)
        {
            var posts = await _postService.GetCompanyPostsAsync(CompanyId);

            return Ok(new BaseResponse
            {
                status = true,
                ErrorCode = 0,
                ErrorMessage = string.Empty,
                Data = posts
            });
        }

        [HttpGet("Comapny/posts")]
        public async Task<IActionResult> GetCompanyPosts()
        {
            var posts = await _postService.GetCompanyPostsAsync(CurrentUser.Id);

            return Ok(new BaseResponse
            {
                status = true,
                ErrorCode = 0,
                ErrorMessage = string.Empty,
                Data = posts
            });
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> DeletePost(string postId)
        {
            var result = await _postService.DeletePostAsync(postId);

            if (result)
            {
                return Ok(new BaseResponse
                {
                    status = true,
                    ErrorCode = 0,
                    ErrorMessage = string.Empty,
                    Data = "Post deleted successfully"
                });
            }

            return BadRequest(new BaseResponse
            {
                status = false,
                ErrorCode = 400,
                ErrorMessage = "Failed to delete post"
            });
        }
    }
}
