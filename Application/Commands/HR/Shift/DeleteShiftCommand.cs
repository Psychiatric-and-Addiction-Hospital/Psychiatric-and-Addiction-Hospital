using Application.Common.Responses;
using Application.DTOS.Responses.HR.Shift;
using MediatR;
using System;

namespace Application.Commands.HR.Shift
{
    public record DeleteShiftCommand(Guid Id)
         : IRequest<BaseResponse<ShiftResponse>>;
}
