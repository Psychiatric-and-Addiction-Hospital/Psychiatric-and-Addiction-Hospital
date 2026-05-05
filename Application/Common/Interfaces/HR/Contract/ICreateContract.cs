using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Contract
{
    public interface ICreateContract
    {
        Task<BaseResponse<ContractResponse>> CreateContractAsync(
         Guid EmployeeId, DateTime StartDate, DateTime? EndDate, string Terms, decimal BaseSalary, CancellationToken ct);
    }
}
