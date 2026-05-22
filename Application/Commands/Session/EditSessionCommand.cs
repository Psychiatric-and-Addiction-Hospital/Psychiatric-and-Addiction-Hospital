using Application.Common.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Session
{
    public record EditSessionCommand(
    Guid SessionId,
    DateTime NewDate,
    int NewDuration) : IRequest<BaseResponse<Unit>>;
}
