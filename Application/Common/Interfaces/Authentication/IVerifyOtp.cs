using Application.Common.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Authentication
{
    public interface IVerifyOtp
    {
        Task<BaseResponse<string>> VerifyOtpAsync(string email, string code, CancellationToken cancellationToken);
    }
}
