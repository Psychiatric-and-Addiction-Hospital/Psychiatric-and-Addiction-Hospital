using Application.Common.Responses;
using Application.DTOS.Request.HR.Position;
using Application.DTOS.Responses.HR;
using MediatR;

namespace Application.Commands.HR.Position
{
    public record UpdatePositionCommand(UpdatePositionRequest request) : IRequest<BaseResponse<PositionResponse>>;
}
