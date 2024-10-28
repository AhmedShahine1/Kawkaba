using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kawkaba.Core.DTO.AuthViewModel
{
    public class DashboardViewModel
    {
        public int CompanyUserCount { get; set; }
        public int EmployeeUserCount { get; set; }
        public int UsersWithoutCompanyCount { get; set; }
        public int PostCount { get; set; }
    }
}
