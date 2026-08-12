using Application.Common.Responses;
using Application.DTOS.Responses.HR.Candidate;
using MediatR;
using System;

namespace Application.Commands.HR.Candidate
{
    public record DeleteCandidateCommand(Guid CandidateId) : IRequest<BaseResponse<CandidateResponse>>;


}
