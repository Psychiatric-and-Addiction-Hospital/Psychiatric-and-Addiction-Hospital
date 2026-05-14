using Application.Common.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Notification
{
    public record MarkNotificationAsReadCommand(Guid NotificationId)
     : IRequest<BaseResponse<Unit>>;
}
