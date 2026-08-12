using Application.Common.Responses;
using Application.DTOS.Request.Authentication;
using MediatR;

namespace Application.Commands.Authentication
{
    public record ChangePasswordCommand(ChangePasswordRequest request) : IRequest<BaseResponse<bool>>;

}
