using Application.Common.Responses;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Session
{
    public record UpdateSessionStatusCommand(
     Guid SessionId,
     SessionStatus NewStatus,
     string? Reason = null) : IRequest<BaseResponse<Unit>>;
}
