using Application.Common.Interfaces;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Application.Queries.Notification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Notificationn
{
    public class GetUserNotificationsHandler : IRequestHandler<GetUserNotificationsQuery, BaseResponse<List<NotificationResponse>>>
    {
        private readonly INotificationService _notificationService;

        public GetUserNotificationsHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<BaseResponse<List<NotificationResponse>>> Handle(GetUserNotificationsQuery request, CancellationToken ct)
        {
            return await _notificationService.GetUserNotificationsAsync(request.UserId);
        }
    }
}
