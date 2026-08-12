using Application.Common.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Application
{
    public interface IDeleteApplication
    {
        Task<BaseResponse<bool>> DeleteAsync(Guid applicationId, CancellationToken ct);
    }
}
