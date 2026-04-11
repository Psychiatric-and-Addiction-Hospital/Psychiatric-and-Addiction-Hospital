using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Employee
{
    public interface IUpdateEmployee
    {
        Task<BaseResponse<EmployeeResponse>> UpdateEmployee(string UserId, string EmployeeCode, string FirstName, string LastName, string Email, Guid DepartmentId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
