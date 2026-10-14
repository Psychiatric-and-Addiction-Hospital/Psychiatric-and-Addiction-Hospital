using Application.Common.Responses;
using Application.DTOS.Request.Authentication;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Authentication
{
    public interface IChangePassword
    {
        Task<BaseResponse<bool>> ChangeAsync(ChangePasswordRequest request, CancellationToken ct);
    }
}
