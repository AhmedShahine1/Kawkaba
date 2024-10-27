using Kawkaba.Core.Entity.ApplicationData;
using Kawkaba.Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kawkaba.Core.Entity.RequestEmployee
{
    public class RequestEmployee
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        public string EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public ApplicationUser Employee { get; set; }

        public string CompanyId {  get; set; }
        [ForeignKey(nameof(CompanyId))]
        public ApplicationUser Company { get; set; }

        public StatusRequestEmployee StatusRequestEmployee { get; set; } = StatusRequestEmployee.Waiting;
    }
}
