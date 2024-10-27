using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kawkaba.BusinessLayer.Interfaces
{
    public interface IAgoraService
    {
        string GenerateToken(string channelName, string userId, int expirationInSeconds = 3600);
    }
}
