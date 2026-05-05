using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;

namespace Application.Queries.HR.Candidate
{
    public record GetCandidateByIdQuery(Guid Id) : IRequest<BaseResponse<CandidateResponse>>;
    
    
}
