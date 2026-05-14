using Application.Commands.Session;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Session;
using Application.Common.Responses;
using Domain.Entites;
using Domain.Entites.Features;
using Domain.Enums;
using Infrastructure.Persistence.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.Sessions
{
    public class SessionService : ISessionService
    {
        private readonly AddIdentityDbContext _context;
        private readonly INotificationService _notificationService;

        public SessionService(AddIdentityDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        public async Task<BaseResponse<Guid>> CreateSessionAsync(CreateSessionCommand command, CancellationToken ct)
        {
            // التحقق من تضارب المواعيد (Conflict Check)
            var isBusy = await _context.Sessions.AnyAsync(s =>
                s.DoctorId == command.DoctorId &&
                s.Status == SessionStatus.Scheduled &&
                s.ScheduledDate < command.ScheduledDate.AddMinutes(command.DurationMinutes) &&
                command.ScheduledDate < s.ScheduledDate.AddMinutes(s.DurationMinutes), ct);

            if (isBusy)
            {
                return ResponseFactory.Fail<Guid>("Time Conflict",
                    new List<string> { "The doctor already has a scheduled session at this time." });
            }

            var session = new Session
            {
                DoctorId = command.DoctorId,
                PatientId = command.PatientId,
                ScheduledDate = command.ScheduledDate,
                DurationMinutes = command.DurationMinutes,
                SessionType = command.SessionType,
                Status = SessionStatus.Scheduled,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Sessions.AddAsync(session, ct);
            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(session.Id, "Session created successfully.");
        }
        public async Task<BaseResponse<Unit>> UpdateNotesAsync(Guid sessionId, string notes, CancellationToken ct)
        {
            // 1. جلب الجلسة
            var session = await _context.Sessions
                .FirstOrDefaultAsync(s => s.Id == sessionId, ct);

            if (session == null)
            {
                return ResponseFactory.Fail<Unit>("Session not found.");
            }

            // 2. تحديث البيانات
            session.Notes = notes;
            session.Status = SessionStatus.Completed; // تحويل الحالة تلقائياً عند إضافة الملاحظات

            // 3. إرسال الإشعار للمريض
            // نفترض أن جدول الجلسات مرتبط بالـ PatientId
            await _notificationService.SendNotificationAsync(
                recipientId: session.PatientId,
                title: "Medical Report Updated",
                message: "Your doctor has updated the notes for your session. You can view them now.",
                type: NotificationType.SessionNoteUpdated,
                relatedId: session.Id
            );

            // 4. حفظ التغييرات
            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(Unit.Value, "Notes updated and patient notified.");
        }

        public async Task<BaseResponse<Unit>> UpdateStatusAsync(UpdateSessionStatusCommand command, CancellationToken ct)
        {
            var session = await _context.Sessions
                .Include(s => s.Reports)
                .Include(s => s.Patient)
                .FirstOrDefaultAsync(s => s.Id == command.SessionId, ct);

            if (session == null)
            {
                return ResponseFactory.Fail<Unit>("Session not found");
            }

            // 1. منطق "إكمال الجلسة" - التأكد من وجود تقرير (Report)
            if (command.NewStatus == SessionStatus.Completed)
            {
                if (!session.Reports.Any())
                {
                    return ResponseFactory.Fail<Unit>("Missing Report",
                        new List<string> { "Cannot complete a psychiatric session without a medical report." });
                }
            }

            // 2. منطق "إلغاء الجلسة" - تسجيل السبب
            if (command.NewStatus == SessionStatus.Cancelled)
            {
                session.CancellationReason = command.Reason;
            }

            // 3. منطق "عدم الحضور" - إرسال تنبيه فوري
            if (command.NewStatus == SessionStatus.Missed)
            {
                var notification = new Notification
                {
                    Title = "Missed Session Alert",
                    Message = $"Patient {session.PatientId} missed their session on {session.ScheduledDate:yyyy-MM-dd}.",
                    SentAt = DateTime.UtcNow,
                    RecipientId = session.DoctorId, // تنبيه الطبيب
                    NotificationType = NotificationType.SessionReminder // أو نوع مخصص
                };
                await _context.Notifications.AddAsync(notification, ct);
            }

            session.Status = command.NewStatus;
            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(Unit.Value, "Status updated successfully.");
        }

        public async Task<BaseResponse<Unit>> EditSessionAsync(EditSessionCommand command, CancellationToken ct)
        {
            var session = await _context.Sessions.FirstOrDefaultAsync(s => s.Id == command.SessionId, ct);

            if (session == null) return ResponseFactory.Fail<Unit>("Session not found");

            if (session.Status != SessionStatus.Scheduled)
            {
                return ResponseFactory.Fail<Unit>("Invalid Operation",
                    new List<string> { "Only scheduled sessions can be modified." });
            }

            session.ScheduledDate = command.NewDate;
            session.DurationMinutes = command.NewDuration;

            await _context.SaveChangesAsync(ct);
            return ResponseFactory.Success(Unit.Value, "Session updated successfully.");
        }
    }
}