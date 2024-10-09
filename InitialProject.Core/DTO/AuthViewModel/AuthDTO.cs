using System.Collections.Generic;

namespace Kawkaba.Core.DTO.AuthViewModel
{
    public class AuthDTO
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public string ProfileImage { get; set; }
        public string ProfileImageId { get; set; }
    }
}
