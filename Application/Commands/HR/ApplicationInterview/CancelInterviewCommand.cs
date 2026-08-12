using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationInterview;
using MediatR;
using System;

namespace Application.Commands.HR.ApplicationInterview
{
    public record CancelInterviewCommand(Guid InterviewId): IRequest<BaseResponse<ApplicationInterviewResponse>>;

}
