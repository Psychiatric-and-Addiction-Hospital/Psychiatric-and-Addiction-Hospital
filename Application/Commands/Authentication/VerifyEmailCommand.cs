using Application.Common.Responses;
using Application.DTOS.Responses.Authentication;
using MediatR;

namespace Application.Commands.Authentication
{
    public record VerifyEmailCommand(string UserId,string Token) : IRequest<BaseResponse<EmailVerificationResponse>>;
}
