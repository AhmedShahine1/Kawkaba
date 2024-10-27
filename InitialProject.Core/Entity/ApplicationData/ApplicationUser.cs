using Kawkaba.Core.Entity.Files;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Kawkaba.Core.Entity.ApplicationData
{
    [DebuggerDisplay("{FullName,nq}")]
    public class ApplicationUser : IdentityUser
    {
        public bool Status { get; set; } = true; // يدل على ما إذا كان الحساب نشطًا أم لا.

        public string FullName { get; set; }
        public string? OTP { get; set; }

        [Range(10000, 99999, ErrorMessage = "Company code must be a 5-digit number.")]
        public int? CompanyCode { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now; // يتم ضبط تاريخ التسجيل تلقائيًا.

        [ForeignKey(nameof(Profile))]
        public string ProfileId { get; set; }
        public Images Profile { get; set; } // صورة الملف الشخصي للمستخدم.

        public string? CompanyId { get; set; } // If the user is an employee, this stores their company's ID
        [ForeignKey(nameof(CompanyId))]
        public ApplicationUser? Company { get; set; } // The company this employee belongs to


        public ICollection<ApplicationUser> Employee { get; set; } = new List<ApplicationUser>();
    }
}
