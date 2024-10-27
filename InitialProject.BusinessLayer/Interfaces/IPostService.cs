using Kawkaba.Core.DTO.AuthViewModel.PostsModel;
using Kawkaba.Core.Entity.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kawkaba.BusinessLayer.Interfaces
{
    public interface IPostService
    {
        Task<PostDTO> CreatePostAsync(PostDTO postDto, string companyId);
        Task<IEnumerable<PostDTO>> GetCompanyPostsAsync(string companyId);
        Task<bool> DeletePostAsync(string postId);
        }
}
