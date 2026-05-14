using Application.Common.Interfaces;
using Application.Common.Responses;
using Application.DTOS.Responses; 
using Domain.Entites.Features;
using Domain.Enums;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore; // ضروري لـ ToListAsync
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.services
{
    public class NotificationService : INotificationService
    {
        private readonly AddIdentityDbContext _context;

        public NotificationService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task SendNotificationAsync(string recipientId, string title, string message, NotificationType type, Guid? relatedId = null)
        {
            var notification = new Notification
            {
                RecipientId = recipientId,
                Title = title,
                Message = message,
                NotificationType = type,
                RelatedSessionId = relatedId, // تم التصحيح من Id إلى relatedId
                SentAt = DateTime.UtcNow, // يفضل استخدام UtcNow في الأنظمة الطبية
                IsRead = false
            };

            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }

        public async Task MarkAsReadAsync(Guid notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<BaseResponse<List<NotificationResponse>>> GetUserNotificationsAsync(string userId)
        {
            var notifications = await _context.Notifications
                .Where(n => n.RecipientId == userId)
                .OrderByDescending(n => n.SentAt)
                .Select(n => new NotificationResponse
                {
                    Id = n.Id,
                    Title = n.Title,
                    Message = n.Message,
                    SentAt = n.SentAt,
                    IsRead = n.IsRead,
                    NotificationType = n.NotificationType.ToString(),
                    RelatedSessionId = n.RelatedSessionId
                })
                .ToListAsync(); // الآن ToListAsync ستعمل بشكل صحيح بعد إضافة Namespace الخاص بـ EF Core

            return ResponseFactory.Success(notifications, "Notifications retrieved successfully.");
        }
    }
}