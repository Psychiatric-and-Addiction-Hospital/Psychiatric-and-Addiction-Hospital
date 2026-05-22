using Application.Commands.Sessions;
using Application.Common.Interfaces.Session;
using Application.Common.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Sessions
{
    public class UpdateSessionNotesHandler : IRequestHandler<UpdateSessionNotesCommand, BaseResponse<Unit>>
    {
        private readonly ISessionService _sessionService;

        public UpdateSessionNotesHandler(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public async Task<BaseResponse<Unit>> Handle(UpdateSessionNotesCommand request, CancellationToken ct)
        {
            return await _sessionService.UpdateNotesAsync(request.SessionId, request.Notes, ct);
        }
    }
}