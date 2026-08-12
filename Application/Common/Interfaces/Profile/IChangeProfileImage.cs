using Application.Common.Responses;
using Application.DTOS.Request.Profile;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Profile
{
    public interface IChangeProfileImage
    {
        Task<BaseResponse<string>> ChangeAsync(ChangeProfileImageRequest request, CancellationToken ct);
    }
}
