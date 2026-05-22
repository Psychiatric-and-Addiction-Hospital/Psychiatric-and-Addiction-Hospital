using Application.Commands.Session;
using Application.Common.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Session
{
    public interface ISessionService
    {
        Task<BaseResponse<Guid>> CreateSessionAsync(CreateSessionCommand command, CancellationToken ct);
        Task<BaseResponse<Unit>> UpdateStatusAsync(UpdateSessionStatusCommand command, CancellationToken ct);
        Task<BaseResponse<Unit>> UpdateNotesAsync(Guid sessionId, string notes, CancellationToken ct);
        Task<BaseResponse<Unit>> EditSessionAsync(EditSessionCommand command, CancellationToken ct);
    }
}
