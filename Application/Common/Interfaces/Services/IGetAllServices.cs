using Application.Common.Responses;
using Application.DTOS.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface IGetAllServices
    {
        Task<BaseResponse<List<ServiceResponse>>> GetAllAsync(CancellationToken ct);
    }
}
