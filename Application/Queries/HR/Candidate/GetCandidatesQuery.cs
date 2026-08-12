using Application.Common.Responses;
using Application.DTOS.Request.HR.Candidate;
using Application.DTOS.Responses.HR.Candidate;
using MediatR;

namespace Application.Queries.HR.Candidate
{
    public record GetCandidatesQuery(
      CandidateListRequest Request) : IRequest<BaseResponse<PagedResponse<CandidateResponse>>>;
}
