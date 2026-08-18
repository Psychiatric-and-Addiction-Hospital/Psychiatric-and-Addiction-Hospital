using Application.Common.Responses;
using Application.DTOS.Request.HR.Candidate;
using Application.DTOS.Responses.HR.Candidate;
using MediatR;

namespace Application.Commands.HR.CandidatePortal
{
    public record CreateCandidateAccountCommand(CreateCandidateAccountRequest Request) : IRequest<BaseResponse<CandidateAccountResponse>>;
}
