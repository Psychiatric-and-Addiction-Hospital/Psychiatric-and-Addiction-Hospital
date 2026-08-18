using Application.Common.Responses;
using Application.DTOS.Responses.HR.Candidate;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.HR.CandidatePortal
{
    public record CandidateInterviewQuery() : IRequest<BaseResponse<List<CandidateInterviewResponse>>>;
}
