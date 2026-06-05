using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;
using System;

namespace Application.Queries.Patient
{
    public record GetSessionDetailsQuery(Guid SessionId) : IRequest<BaseResponse<SessionDetailResponse>>;
}
