using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Contract
{
    public interface IGetContractById
    {
        Task<BaseResponse<ContractResponse>> GetContractByIdAsync(Guid contractId, CancellationToken ct);
    }
}
