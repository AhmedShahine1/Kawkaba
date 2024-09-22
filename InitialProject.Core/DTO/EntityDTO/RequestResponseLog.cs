using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Core.DTO.EntityDTO
{
    public class RequestResponseLog
    {
        public string RequestUrl { get; set; }
        public string HttpMethod { get; set; }
        public string RequestBody { get; set; }
        public string ResponseBody { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
