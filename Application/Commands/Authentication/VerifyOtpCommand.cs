using Application.Common.Responses;
using MediatR;


namespace Application.Commands.Authentication
{
    public record VerifyOtpCommand(string Email, string Code)
        : IRequest<BaseResponse<string>>;


}
