using Application.Commands.Authentication;
using Application.Common.Interfaces.Authentication;
using Application.Common.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Application.Handlers.Authentication
{
    public class SendForgetPasswordOtpHandler : IRequestHandler<SendForgetPasswordOtpCommand, BaseResponse<string>>
    {
        private readonly IPasswordResetService _passwordService;
        public SendForgetPasswordOtpHandler(IPasswordResetService passwordService)
        {
            _passwordService = passwordService;
        }
        public async Task<BaseResponse<string>> Handle(SendForgetPasswordOtpCommand request, CancellationToken ct)
        {
            return await _passwordService.SendForgetPasswordOtpAsync(request.Email,ct);
        }
    }
}
