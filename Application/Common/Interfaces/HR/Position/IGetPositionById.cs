using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Position
{
    public interface IGetPositionById
    {
        Task<BaseResponse<PositionResponse>> GetByIdAsync(Guid id,CancellationToken ct);
    }
}
