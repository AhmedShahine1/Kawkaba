using Kawkaba.BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kawkaba.Core.DTO.AuthViewModel.RequesrLog;

namespace Kawkaba.BusinessLayer.Services
{
    public class RequestResponseService : IRequestResponseService
    {
        private readonly List<RequestResponseLog> _logs = new();

        public Task AddLogAsync(RequestResponseLog log)
        {
            _logs.Add(log);
            return Task.CompletedTask;
        }

        public Task<List<RequestResponseLog>> GetAllLogsAsync()
        {
            return Task.FromResult(_logs);
        }
    }
}
