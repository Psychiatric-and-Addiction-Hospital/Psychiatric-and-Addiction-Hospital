using Application.Common.Responses;
using Application.DTOS.Responses.HR.Employee;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Employee
{
    public interface IGetEmployeeById
    {
        Task<BaseResponse<EmployeeResponse>> GetByIdAsync(Guid employeeId, CancellationToken ct);
    }
}
