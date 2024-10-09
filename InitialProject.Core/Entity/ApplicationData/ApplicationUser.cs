using Kawkaba.Core.Entity.Files;
using Microsoft.AspNetCore.Identity;
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

        public DateTime RegistrationDate { get; set; } = DateTime.Now; // يتم ضبط تاريخ التسجيل تلقائيًا.

        [ForeignKey(nameof(Profile))]
        public string ProfileId { get; set; }

        public Images Profile { get; set; } // صورة الملف الشخصي للمستخدم.
    }
}
