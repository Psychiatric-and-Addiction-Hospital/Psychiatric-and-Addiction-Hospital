using Application.Common.Responses;
using Application.DTOS.Request.HR.Position;
using Application.DTOS.Responses.HR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Position
{
    public interface IUpdatePosition
    {
        Task<BaseResponse<PositionResponse>> UpdatePositionAsync(UpdatePositionRequest request, CancellationToken ct);
    }
}
