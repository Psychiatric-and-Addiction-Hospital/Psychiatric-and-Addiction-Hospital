using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;

namespace Application.Queries.HR.Position
{
    public record GetPositionByIdQuery(Guid Id) : IRequest<BaseResponse<PositionResponse>>;
}
