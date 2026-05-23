using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Employee
{
    public interface ICreateEmployee

    {
        Task<BaseResponse<EmployeeResponse>> CreateAsync
            (string EmployeeCode, string FirstName, string LastName,string Email, Guid DepartmentId, CancellationToken ct);

    }
}
