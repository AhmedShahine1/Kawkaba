using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kawkaba.Core.DTO.AuthViewModel.RoleModel
{
    public class RoleUserModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public IEnumerable<string> RoleId { get; set; }
    }
}
