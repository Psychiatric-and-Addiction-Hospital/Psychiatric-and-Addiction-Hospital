using Application.Common.Responses;
using Application.DTOS.Request.HR.Contract;
using Application.DTOS.Responses.HR.Contract;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Contract
{
    public interface ICreateContract
    {
        Task<BaseResponse<ContractResponse>> CreateAsync(CreateContractRequest request,CancellationToken ct);
    }
}
