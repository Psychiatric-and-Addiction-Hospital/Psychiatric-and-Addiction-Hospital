using Application.Common.Responses;
using Application.DTOS.Responses.HR.LeaveType;
using MediatR;
using System;

namespace Application.Queries.HR.LeaveType
{
    public record GetleaveTypeByIdQuery(Guid Id) : IRequest<BaseResponse<LeaveTypeResponse>>;

}
