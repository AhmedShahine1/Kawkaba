using AgoraIO.Media;
using Azure.Core;
using Kawkaba.BusinessLayer.Interfaces;
using Kawkaba.Core.Helpers;
using Microsoft.Extensions.Options;

namespace Kawkaba.BusinessLayer.Services
{
    public class AgoraService : IAgoraService
    {
        private readonly string _appId;
        private readonly string _appCertificate;

        public AgoraService(IOptions<AgoraSettings> agoraSettings)
        {
            _appId = agoraSettings.Value.AppId;
            _appCertificate = agoraSettings.Value.AppCertificate;
        }

        public string GenerateToken(string channelName, string userId, int expirationInSeconds = 3600)
        {
            var tokenBuilder = new AgoraIO.Media.AccessToken(_appId, _appCertificate, channelName, userId);
            tokenBuilder.addPrivilege(Privileges.kJoinChannel, (uint)expirationInSeconds);
            tokenBuilder.addPrivilege(Privileges.kPublishAudioStream, (uint)expirationInSeconds); // Add other privileges if needed
            return tokenBuilder.build();
        }
    }
}
