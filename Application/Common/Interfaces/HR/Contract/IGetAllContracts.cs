using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Contract
{
    public interface IGetAllContracts
    {
        Task<BaseResponse<List<ContractResponse>>> GetAllContractsAsync(CancellationToken ct);
    }
}
