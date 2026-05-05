using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace Application.Commands.HR.Candidate
{
    public record UpdateCandidateCommand(Guid CandidateId,
    string FullName,
    string Email,
    string Phone,
    IFormFile ResumeUrl) : IRequest<BaseResponse<CandidateResponse>>;


}
