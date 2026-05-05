using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Domain.Enums;
using MediatR;
using System;

namespace Application.Commands.HR.ApplicationInterview
{
    public record CreateApplicationInterviewCommand(Guid ApplicationProcessId, DateTime ScheduledTime,
        string InterviewerName, InterviewType InterviewType, string Location) : IRequest<BaseResponse<ApplicationInterviewResponse>>;

}
