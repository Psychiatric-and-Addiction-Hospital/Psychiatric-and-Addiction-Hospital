using Application.Common.Responses;
using MediatR;
using System;

namespace Application.Commands.Sessions
{
    public record UpdateSessionNotesCommand(
        Guid SessionId,
        string Notes) : IRequest<BaseResponse<Unit>>;
}