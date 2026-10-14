using Application.Common.Responses;
using Application.DTOS.Request.HR.Application;
using Application.DTOS.Responses.HR.Application;
using MediatR;

namespace Application.Commands.HR.Application
{

    public record CreateApplicationCommand(
        CreateApplicationRequest Request)
        : IRequest<BaseResponse<ApplicationResponse>>;
}
