using Application.Commands.Authentication;
using Application.Common.Interfaces.Authentication;
using Application.Common.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Authentication
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, BaseResponse<bool>>
    {
        private readonly IChangePassword _service;
        public ChangePasswordHandler(IChangePassword service)
        {
            _service = service;
        }
        public async Task<BaseResponse<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            return await _service.ChangeAsync(request.request, cancellationToken);
        }
    }
}
