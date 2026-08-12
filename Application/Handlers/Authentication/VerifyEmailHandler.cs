using Application.Commands.Authentication;
using Application.Common.Interfaces.Authentication;
using Application.Common.Responses;
using Application.DTOS.Responses.Authentication;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Authentication
{
    public class VerifyEmailHandler : IRequestHandler<VerifyEmailCommand, BaseResponse<EmailVerificationResponse>>
    {
        private readonly IEmailVerificationService _emailVerificationService;

        public VerifyEmailHandler(
            IEmailVerificationService emailVerificationService)
        {
            _emailVerificationService = emailVerificationService;
        }
        public async Task<BaseResponse<EmailVerificationResponse>> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            return await _emailVerificationService.VerifyEmailAsync(request.UserId, request.Token, cancellationToken);
        }
    }
}
