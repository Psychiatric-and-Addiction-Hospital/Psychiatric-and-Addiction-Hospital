using Application.Common.Responses;
using Application.DTOS.Responses.HR.Contract;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.CandidatePortal
{
    public interface ISignContract
    {
        Task<BaseResponse<ContractResponse>> SignContractAsync(Guid ContractId, CancellationToken ct);
    }
}
