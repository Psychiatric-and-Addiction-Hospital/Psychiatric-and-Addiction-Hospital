using Application.Common.Responses;
using Application.DTOS.Responses.HR.Contract;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Contract
{
    public interface ISubmitContractForSignature
    {
        Task<BaseResponse<ContractResponse>> SubmitContractForSignatureAsync(Guid ContractId,CancellationToken ct);
    }
}
