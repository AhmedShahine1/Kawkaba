using Kawkaba.Core.Entity.ApplicationData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kawkaba.Core.DTO.AuthViewModel.RequestEmployeeModel
{
    public class RequestEmployeeDTO
    {
        public string Id { get; set; }

        public AuthDTO Employee {  get; set; }

        public AuthDTO Company {  get; set; }
        
    }
}
