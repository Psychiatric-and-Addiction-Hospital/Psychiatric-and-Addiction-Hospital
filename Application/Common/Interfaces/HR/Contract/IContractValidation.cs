using Application.Common.Responses;
using Application.DTOS.Request.HR.Contract;
using System;
using System.Threading;
using System.Threading.Tasks;
using ContractEntity = Domain.Entites.HR.Contract;

namespace Application.Common.Interfaces.HR.Contract
{
    public interface IContractValidation
    {
        Task<BaseResponse<bool>> ValidateCreateAsync(CreateContractRequest request, CancellationToken ct);

        Task<BaseResponse<ContractEntity>> ValidateUpdateAsync(UpdateContractRequest request, CancellationToken ct);

        Task<BaseResponse<ContractEntity>> ValidateSubmitAsync(Guid contractId, CancellationToken ct);

        Task<BaseResponse<ContractEntity>> ValidateSignAsync(Guid contractId, CancellationToken ct);

        Task<BaseResponse<ContractEntity>> ValidateCancelAsync(Guid contractId, CancellationToken ct);
    }
}
