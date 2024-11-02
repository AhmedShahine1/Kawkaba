using Kawkaba.Core.DTO.AuthViewModel.PostsModel;
using Kawkaba.Core.DTO.AuthViewModel;
using Kawkaba.Core.Entity.ApplicationData;
using Kawkaba.Core.Entity.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kawkaba.BusinessLayer.Interfaces
{
    public interface IDashboardService
    {
        int GetCompanyUserCountAsync();
        int GetEmployeeUserCountAsync();
        int GetUsersWithoutCompanyCountAsync();
        Task<int> GetPostCountAsync();
        Task<IEnumerable<AuthDTO>> GetAllUsersAsync();
        Task<IEnumerable<AuthDTO>> GetAllCompaniesAsync();
        Task<IEnumerable<AuthDTO>> GetAllEmployeesAsync();
        Task<IEnumerable<PostDTO>> GetAllPostsAsync();
    }
}
