using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Notification
{
    public record GetUserNotificationsQuery(string UserId)
     : IRequest<BaseResponse<List<NotificationResponse>>>;
}
