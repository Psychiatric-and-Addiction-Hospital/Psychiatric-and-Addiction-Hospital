using Application.Common.Responses;
using MediatR;

namespace Application.Commands.Authentication
{
    public record SendForgetPasswordOtpCommand(string Email
    ) : IRequest<BaseResponse<string>>;
}
