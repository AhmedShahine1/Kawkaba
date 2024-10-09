using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kawkaba.Core.DTO.AuthViewModel.RoleModel
{
    [DebuggerDisplay("{RoleName,nq}")]
    public class RoleDTO
    {        
        [Required, Display(Name = "Role Name"), StringLength(50)]
        public string RoleName { get; set; }
        [Required, Display(Name = "Role Description"), StringLength(int.MaxValue)]
        public string RoleDescription { get; set; }
        [Required, Display(Name = "Role Name Arabic"), StringLength(50)]
        public string RoleAr { get; set; }
    }
}
