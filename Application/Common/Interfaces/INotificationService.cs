using Application.Common.Responses;
using Application.DTOS.Responses;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string recipientId, string title, string message, NotificationType type, Guid? relatedId = null);
        Task MarkAsReadAsync(Guid notificationId);
        Task<BaseResponse<List<NotificationResponse>>> GetUserNotificationsAsync(string userId);
    }
}
