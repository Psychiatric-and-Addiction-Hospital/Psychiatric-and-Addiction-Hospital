using Application.Commands.Profile;
using Application.Common.Interfaces.Profile;
using Application.Common.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Profile
{
    public class ChangeProfileImageHandler : IRequestHandler<ChangeProfileImageCommand, BaseResponse<string>>
    {
        private readonly IChangeProfileImage _service;
        public ChangeProfileImageHandler(IChangeProfileImage service)
        {
            _service = service;
        }

        public async Task<BaseResponse<string>> Handle(ChangeProfileImageCommand request, CancellationToken ct)
        {
            return await _service.ChangeAsync(request.request, ct);
        }
    }
}
