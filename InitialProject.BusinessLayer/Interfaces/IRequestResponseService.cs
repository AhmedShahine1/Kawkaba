using InitialProject.Core.DTO.EntityDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.BusinessLayer.Interfaces
{
    public interface IRequestResponseService
    {
        Task AddLogAsync(RequestResponseLog log);
        Task<List<RequestResponseLog>> GetAllLogsAsync();
    }
}
