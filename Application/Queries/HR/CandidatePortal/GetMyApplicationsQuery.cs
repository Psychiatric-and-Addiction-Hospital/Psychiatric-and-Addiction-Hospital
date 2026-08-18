using Application.Common.Responses;
using Application.DTOS.Responses.HR.Application;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.HR.CandidatePortal
{
    public record GetMyApplicationsQuery (): IRequest<BaseResponse<List<ApplicationResponse>>>;
}
