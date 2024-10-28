using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kawkaba.BusinessLayer.Interfaces
{
    public interface IDashboardService
    {
        Task<int> GetCompanyUserCountAsync();
        Task<int> GetEmployeeUserCountAsync();
        Task<int> GetUsersWithoutCompanyCountAsync();
        Task<int> GetPostCountAsync();
    }
}
