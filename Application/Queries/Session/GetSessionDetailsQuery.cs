using Application.Common.Responses;
using Application.DTOs.Responses.Sessions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Session
{
    public record GetSessionDetailsQuery(Guid SessionId)
    : IRequest<BaseResponse<SessionDetailsResponse>>;
}
