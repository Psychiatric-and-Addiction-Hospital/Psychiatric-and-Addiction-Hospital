using Application.Common.Responses;
using Application.DTOS.Request.HR.ApplicationInterview;
using Application.DTOS.Responses.HR.ApplicationInterview;
using MediatR;

namespace Application.Commands.HR.ApplicationInterview
{
    public record CreateApplicationInterviewCommand
        (CreateApplicationInterviewRequest Request) : IRequest<BaseResponse<ApplicationInterviewResponse>>;

}
