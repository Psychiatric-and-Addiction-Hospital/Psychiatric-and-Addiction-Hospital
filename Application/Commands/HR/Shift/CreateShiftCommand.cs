using Application.Common.Responses;
using Application.DTOS.Request.HR.Shift;
using Application.DTOS.Responses.HR.Shift;
using MediatR;
using System;

namespace Application.Commands.HR.Shift
{
    public record CreateShiftCommand(CreateShiftRequest request) : IRequest<BaseResponse<ShiftResponse>>;
}
