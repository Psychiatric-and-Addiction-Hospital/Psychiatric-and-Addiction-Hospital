using Application.Common.Responses;
using Application.DTOS.Request.HR.ApplicationInterview;
using Application.DTOS.Responses.HR.ApplicationInterview;
using MediatR;

namespace Application.Commands.HR.ApplicationInterview
{
    public record UpdateApplicationInterviewCommand(UpdateApplicationInterviewRequest Request)
      : IRequest<BaseResponse<ApplicationInterviewResponse>>;
}
