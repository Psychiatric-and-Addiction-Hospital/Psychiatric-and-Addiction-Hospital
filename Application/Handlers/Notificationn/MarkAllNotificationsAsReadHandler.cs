using Application.Commands.Notification;
using Application.Common.Interfaces;
using Application.Common.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Notificationn
{
    public class MarkAllNotificationsAsReadHandler : IRequestHandler<MarkAllNotificationsAsReadCommand, BaseResponse<Unit>>
    {
        private readonly INotificationService _notificationService;

        public MarkAllNotificationsAsReadHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<BaseResponse<Unit>> Handle(MarkAllNotificationsAsReadCommand request, CancellationToken ct)
        {
            await _notificationService.MarkAllAsReadAsync(request.UserId);
            return ResponseFactory.Success(Unit.Value, "All notifications marked as read");
        }
    }
}
