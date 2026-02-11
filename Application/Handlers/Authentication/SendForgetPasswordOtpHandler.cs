using Application.Commands.Authentication;
using Application.Common.Interfaces;
using Domain.Entites;
using FluentResults;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Application.Handlers.Authentication
{
    public class SendForgetPasswordOtpHandler : IRequestHandler<SendForgetPasswordOtpCommand, Result<string>>
    {
        private readonly IPasswordResetService _passwordService;
        public SendForgetPasswordOtpHandler(IPasswordResetService passwordService)
        {
            _passwordService = passwordService;
        }
        public async Task<Result<string>> Handle(SendForgetPasswordOtpCommand request, CancellationToken cancellationToken)
        {
            return await _passwordService.SendForgetPasswordOtpAsync(request.Email);
        }
    }
}
