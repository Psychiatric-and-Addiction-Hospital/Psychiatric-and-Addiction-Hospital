using Application.Common.Responses;
using Application.DTOS.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface IGetServiceById
    {
        Task<BaseResponse<ServiceResponse>> GetByIdAsync(Guid id, CancellationToken ct);
    }
}
