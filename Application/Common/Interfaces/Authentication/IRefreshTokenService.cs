using Application.Common.Responses;
using Application.DTOS.Responses;
using Domain.Entites;
using FluentResults;

using System.Threading.Tasks;

namespace Application.Common.Interfaces.Authentication
{
    public interface IRefreshTokenService
    {
        Task<string> GenerateRefreshTokenAsync(AppUser user);
        Task<BaseResponse<AuthResult>> RefreshAsync(string refreshToken);
    }
}
