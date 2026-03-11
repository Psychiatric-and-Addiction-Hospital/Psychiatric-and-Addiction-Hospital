using Application.Commands.Authentication;
using Application.Common.Interfaces.Authentication;
using Application.Common.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Authentication
{
    public class VerifyOtpHandler:IRequestHandler<VerifyOtpCommand, BaseResponse<string>>
    {
        private readonly IVerifyOtp _VerifyOtp;

        public VerifyOtpHandler(IVerifyOtp VerifyOtp)
        {
            _VerifyOtp = VerifyOtp;
        }

        public async Task<BaseResponse<string>> Handle(VerifyOtpCommand request, CancellationToken ct)
        {
            return await _VerifyOtp.VerifyOtpAsync(request.Email, request.Code, ct);
        }
    }
}
