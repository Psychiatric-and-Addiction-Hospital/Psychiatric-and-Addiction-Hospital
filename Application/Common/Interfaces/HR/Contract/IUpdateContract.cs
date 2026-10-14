using Application.Common.Responses;
using Application.DTOS.Request.HR.Contract;
using Application.DTOS.Responses.HR.Contract;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Contract
{
    public interface IUpdateContract
    {
        Task<BaseResponse<ContractResponse>> UpdateAsync(UpdateContractRequest request,CancellationToken ct);
    }
}
