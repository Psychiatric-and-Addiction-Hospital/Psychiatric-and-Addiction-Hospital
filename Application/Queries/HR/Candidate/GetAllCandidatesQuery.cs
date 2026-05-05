using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.HR.Candidate
{
    public record GetAllCandidatesQuery() : IRequest<BaseResponse<List<CandidateResponse>>>;
  
}
