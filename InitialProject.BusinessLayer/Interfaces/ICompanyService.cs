using Kawkaba.Core.DTO.AuthViewModel.RequestEmployeeModel;
using Kawkaba.Core.Entity.ApplicationData;
using Kawkaba.Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kawkaba.BusinessLayer.Interfaces
{
    public interface ICompanyService
    {
        Task<bool> RequestToJoinCompany(string employeeId, int CompanyCode);
        Task<bool> UpdateStatusRequest(string requestId, StatusRequestEmployee statusRequestEmployee);
        Task<List<ApplicationUser>> GetCompanyEmployees(string companyId);
        Task<List<RequestEmployeeDTO>> GetRequestCompany(string companyId);
        Task<bool> RemoveEmployee(string employeeId);
    }
}
