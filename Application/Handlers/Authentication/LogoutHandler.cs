using Application.Commands.Authentication;
using Application.Common.Interfaces.Authentication;
using Application.Common.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Authentication
{
    public class LogoutHandler : IRequestHandler<LogoutCommand, BaseResponse<Unit>>
    {
        private readonly IRefreshTokenService _refreshTokenService;

        public LogoutHandler(IRefreshTokenService refreshTokenService)
        {
            _refreshTokenService = refreshTokenService;
        }

        public async Task<BaseResponse<Unit>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            await _refreshTokenService.RevokeAllUserTokensAsync(request.UserId);
            return ResponseFactory.Success(Unit.Value, "Logged out successfully");
        }
    }
}
