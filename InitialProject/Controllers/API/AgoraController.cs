using Kawkaba.BusinessLayer.Interfaces;
using Kawkaba.Core.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Kawkaba.Controllers.API
{
    public class AgoraController : BaseController
    {
        private readonly IAgoraService _agoraservice;
        private readonly string _appId;
        private readonly string _appCertificate;

        public AgoraController(IAgoraService agoraService, IOptions<AgoraSettings> agoraSettings)
        {
            _agoraservice = agoraService;
            _appId = agoraSettings.Value.AppId;
            _appCertificate = agoraSettings.Value.AppCertificate;
        }

        [HttpPost("CreateChannel")]
        public IActionResult CreateChannel([FromQuery] string channelName, [FromQuery] string userId)
        {
            if (string.IsNullOrEmpty(channelName) || string.IsNullOrEmpty(userId))
            {
                return BadRequest("Channel name and user ID are required");
            }

            try
            {
                string token = _agoraservice.GenerateToken(channelName, userId);

                return Ok(new
                {
                    Token = token,
                    ChannelName = channelName,
                    UserId = userId,
                    AppId = _appId
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error generating token: {ex.Message}");
            }
        }
    }
}
