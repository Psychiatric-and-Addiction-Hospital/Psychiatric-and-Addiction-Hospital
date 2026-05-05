using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;

namespace Application.Commands.HR.ApplicationInterview
{
    public record UpdateInterviewResultCommand(Guid InterviewId, string Feedback, int Score)
        : IRequest<BaseResponse<ApplicationInterviewResponse>>;

}
