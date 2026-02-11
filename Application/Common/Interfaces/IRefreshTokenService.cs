using Application.DTOS.Responses;
using Domain.Entites;
using FluentResults;

using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<string> GenerateRefreshTokenAsync(AppUser user);
        Task<Result<AuthResult>> RefreshAsync(string refreshToken);
    }
}
