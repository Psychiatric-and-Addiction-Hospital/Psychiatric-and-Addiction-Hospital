using Application.Commands.Authentication;
using Application.Common.Interfaces;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Authentication
{
    public class VerifyOtpHandler:IRequestHandler<VerifyOtpCommand, Result<string>>
    {
        private readonly IVerifyOtp _VerifyOtp;

        public VerifyOtpHandler(IVerifyOtp VerifyOtp)
        {
            _VerifyOtp = VerifyOtp;
        }

        public async Task<Result<string>> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
        {
            return await _VerifyOtp.VerifyOtpAsync(request.Email, request.Code, cancellationToken);
        }
    }
}
