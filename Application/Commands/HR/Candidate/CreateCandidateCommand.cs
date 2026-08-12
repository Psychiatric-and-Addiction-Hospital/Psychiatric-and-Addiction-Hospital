using Application.Common.Responses;
using Application.DTOS.Request.HR.Candidate;
using Application.DTOS.Responses.HR.Candidate;
using MediatR;

namespace Application.Commands.HR.Candidate
{
    public record CreateCandidateCommand(CreateCandidateRequest Request) : 
        IRequest<BaseResponse<CandidateResponse>>;
}
