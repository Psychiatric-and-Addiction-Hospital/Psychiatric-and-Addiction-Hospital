using Application.Commands.Session;
using Application.Common.Interfaces.Session;
using Application.Common.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Session
{
    public class UpdateSessionStatusHandler : IRequestHandler<UpdateSessionStatusCommand, BaseResponse<Unit>>
    {
        private readonly ISessionService _sessionService;

        public UpdateSessionStatusHandler(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public async Task<BaseResponse<Unit>> Handle(UpdateSessionStatusCommand request, CancellationToken ct)
        {
            return await _sessionService.UpdateStatusAsync(request, ct);
        }
    }
}
