using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;

namespace Application.Commands.HR.Position
{
    public record DeletePositionCommand(Guid Id)
        : IRequest<BaseResponse<PositionResponse>>;
}
