using Application.Common.Responses;
using Application.DTOS.Responses.Authentication;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Authentication
{
    public interface IEmailVerificationService
    {
        Task<BaseResponse<bool>> SendVerificationEmailAsync(string userId, CancellationToken ct);

        Task<BaseResponse<EmailVerificationResponse>> VerifyEmailAsync(string userId, string token, CancellationToken ct);

        Task<BaseResponse<bool>> ResendVerificationEmailAsync(string email, CancellationToken ct);
    }
}
