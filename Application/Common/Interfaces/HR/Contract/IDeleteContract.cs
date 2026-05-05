using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Contract
{
    public interface IDeleteContract
    {
        Task<BaseResponse<ContractResponse>> DeleteContractAsync(Guid contractId, CancellationToken ct);
    }
}
