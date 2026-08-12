using Application.Commands.Authentication;
using Application.Common.Interfaces.Authentication;
using Application.Common.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Authentication
{
    public class ResendVerificationEmailHandler : IRequestHandler<ResendVerificationEmailCommand, BaseResponse<bool>>
    {
        private readonly IEmailVerificationService _emailVerificationService;

        public ResendVerificationEmailHandler(IEmailVerificationService emailVerificationService)
        {
            _emailVerificationService = emailVerificationService;
        }

        public async Task<BaseResponse<bool>> Handle(ResendVerificationEmailCommand request, CancellationToken cancellationToken)
        {
            return await _emailVerificationService.ResendVerificationEmailAsync(request.Email, cancellationToken);
        }
    }
}
