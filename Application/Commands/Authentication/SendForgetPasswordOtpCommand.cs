using FluentResults;
using MediatR;

namespace Application.Commands.Authentication
{
    public record SendForgetPasswordOtpCommand(string Email
    ) : IRequest<Result<string>>;
}
