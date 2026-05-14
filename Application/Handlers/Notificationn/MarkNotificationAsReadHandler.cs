using Application.Commands.Notification;
using Application.Common.Interfaces;
using Application.Common.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Notificationn
{
    public class MarkNotificationAsReadHandler : IRequestHandler<MarkNotificationAsReadCommand, BaseResponse<Unit>>
    {
        private readonly INotificationService _notificationService;

        public MarkNotificationAsReadHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<BaseResponse<Unit>> Handle(MarkNotificationAsReadCommand request, CancellationToken ct)
        {
            await _notificationService.MarkAsReadAsync(request.NotificationId);
            return ResponseFactory.Success(Unit.Value, "Notification marked as read");
        }
    }
}
