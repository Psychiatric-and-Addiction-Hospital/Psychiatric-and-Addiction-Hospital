using Application.Common.Responses;
using Application.DTOS.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface IDeleteService
    {
        Task<BaseResponse<ServiceResponse>> DeleteAsync(Guid id, CancellationToken ct);
    }
}
