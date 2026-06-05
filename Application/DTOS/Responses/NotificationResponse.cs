using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Responses
{
    public record NotificationResponse
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Message { get; init; }
        public DateTime SentAt { get; init; }
        public bool IsRead { get; init; }

        // نوع الإشعار كنص لعرضه أو استخدامه في اختيار الأيقونة في الـ Front-end
        public string NotificationType { get; init; }

        // معرف الجلسة المرتبطة (إن وجد) لتمكين الانتقال السريع (Navigation)
        public Guid? RelatedSessionId { get; init; }

        // حقل إضافي مفيد للـ Front-end لتحديد لون أو شكل الإشعار
        // مثلاً: Success, Warning, Info, Danger
        public string Severity => GetSeverity();

        private string GetSeverity()
        {
            return NotificationType switch
            {
                "BookingRejected" or "SessionCancelled" => "Danger",
                "BookingApproved" or "SessionCompleted" => "Success",
                "SessionReminder" or "NewBookingRequest" => "Warning",
                _ => "Info"
            };
        }
    }
}
