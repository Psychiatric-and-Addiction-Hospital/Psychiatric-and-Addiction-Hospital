using Application.Common.Responses;
using Application.DTOS.Responses.HR.Application;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Application
{
    public interface IGetApplicationById
    {
        Task<BaseResponse<ApplicationResponse>> GetByIdAsync(Guid id, CancellationToken ct);
    }
}
