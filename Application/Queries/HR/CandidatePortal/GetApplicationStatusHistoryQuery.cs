using Application.Common.Responses;
using Application.DTOS.Responses.HR.Application;
using MediatR;
using System;
using System.Collections.Generic;

namespace Application.Queries.HR.CandidatePortal
{
    public record GetApplicationStatusHistoryQuery(Guid ApplicationId) : IRequest<BaseResponse<List<ApplicationStatusHistoryResponse>>>;
}
