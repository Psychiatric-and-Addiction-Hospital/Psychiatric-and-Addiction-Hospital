using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;

namespace Application.Commands.HR.ApplicationProcess
{
    public record CreateApplicationProcessCommand(Guid CandidateId, Guid RecruitmentId) 
        : IRequest<BaseResponse<ApplicationProcessResponse>>;
   
}
