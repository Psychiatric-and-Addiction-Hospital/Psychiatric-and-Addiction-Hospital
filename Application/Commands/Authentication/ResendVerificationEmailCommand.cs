using Application.Common.Responses;
using MediatR;

namespace Application.Commands.Authentication
{
    public record ResendVerificationEmailCommand(string Email) : IRequest<BaseResponse<bool>>;
}
