using Application.Commands.Authentication;
using Application.Common.Interfaces;
using Application.DTOS.Responses;
using FluentResults;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Authentication
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, Result<AuthResult>>
    {
        private readonly IRefreshTokenService _service;

        public RefreshTokenHandler(IRefreshTokenService service)
        {
            _service = service;
        }
        public async Task<Result<AuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await _service.RefreshAsync(request.RefreshToken);
        }
    }
}
