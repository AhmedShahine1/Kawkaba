using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kawkaba.Core.DTO
{
    public class EmailRequest
    {
        [Required]
        [EmailAddress(ErrorMessage = "The email address is not valid.")]
        public string ToEmail { get; set; }
    }
}
