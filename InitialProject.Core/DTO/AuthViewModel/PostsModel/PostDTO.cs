using Kawkaba.Core.Entity.Files;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kawkaba.Core.DTO.AuthViewModel.PostsModel
{
    public class PostDTO
    {
        public string? Id { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public AuthDTO Auth { get; set; }
        public IEnumerable<IFormFile>? Files { get; set; }
        public IEnumerable<string>? FileUrls { get; set; } // For returning URLs of uploaded files
    }
}
