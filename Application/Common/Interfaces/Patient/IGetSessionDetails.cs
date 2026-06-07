using Application.Common.Responses;
using Application.DTOS.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Patient
{
    public interface IGetSessionDetails
    {
        Task<BaseResponse<SessionDetailResponse>> GetDetailsAsync(Guid sessionId, CancellationToken ct);
    }
}
