using Application.Common.Responses;
using Application.DTOS.Responses.HR.Candidate;
using MediatR;

namespace Application.Queries.HR.CandidatePortal
{

    public record GetCandidateDashboardQuery() : IRequest<BaseResponse<CandidateDashboardResponse>>;
}
