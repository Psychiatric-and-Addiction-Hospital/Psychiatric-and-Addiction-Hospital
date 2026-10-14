using Application.Common.Responses;
using Application.DTOS.Responses.HR.Shift;
using MediatR;
using System;

namespace Application.Queries.HR.Shift
{
    public record GetShiftByIdQuery(Guid Id)
       : IRequest<BaseResponse<ShiftResponse>>;
}
