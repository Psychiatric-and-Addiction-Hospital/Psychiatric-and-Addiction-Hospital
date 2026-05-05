using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Commands.HR.Candidate
{
    public record CreateCandidateCommand(string FullName,string Email,string Phone, IFormFile ResumeUrl) : 
        IRequest<BaseResponse<CandidateResponse>>;
}
