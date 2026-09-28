using Application.Commands.Patient;
using Application.Common.Interfaces.Patient;
using Application.Common.Responses;
using Application.DTOS.Responses.Session;
using Application.DTOs.Responses.Sessions;
using Domain.Entites.DoctorsModule;
using MediatR;
using Domain.Enums;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.services.Patient
{
    public class MyAppointmentsService : IMyAppointmentsService
    {
        private readonly AddIdentityDbContext _context;
        public MyAppointmentsService(AddIdentityDbContext context) => _context = context;

        // ── GET list ────────────────────────────────────────────────────────────
        public async Task<BaseResponse<List<SessionSummaryResponse>>> GetAppointmentsAsync(
            string patientId, string status, CancellationToken ct)
        {
            var query = _context.Sessions
                .Include(s => s.Doctor)
                .Where(s => s.PatientId == patientId);

            query = status?.ToLower() switch
            {
                "upcoming"  => query.Where(s => s.Status == SessionStatus.Scheduled && s.ScheduledDate >= DateTime.UtcNow),
                "completed" => query.Where(s => s.Status == SessionStatus.Completed),
                "cancelled" => query.Where(s => s.Status == SessionStatus.Cancelled),
                _           => query   // "all" أو أي قيمة تانية
            };

            var sessions = await query
                .OrderByDescending(s => s.ScheduledDate)
                .Select(s => new SessionSummaryResponse
                {
                    Id            = s.Id,
                    ScheduledDate = s.ScheduledDate,
                    DurationMinutes = s.DurationMinutes,
                    SessionType   = s.SessionType.ToString(),
                    Status        = s.Status.ToString(),
                    DoctorId      = s.DoctorId,
                    DoctorName    = s.Doctor != null ? $"{s.Doctor.FirstName} {s.Doctor.LastName}" : "Unknown"
                })
                .ToListAsync(ct);

            return ResponseFactory.Success(sessions, $"{sessions.Count} appointment(s) found");
        }

        // ── GET single ──────────────────────────────────────────────────────────
        public async Task<BaseResponse<SessionDetailsResponse>> GetAppointmentDetailsAsync(
            Guid sessionId, string patientId, CancellationToken ct)
        {
            var s = await _context.Sessions
                .Include(s => s.Doctor)
                .Include(s => s.Patient)
                .Include(s => s.Reports)
                .Include(s => s.ProgressTrackers)
                .FirstOrDefaultAsync(s => s.Id == sessionId && s.PatientId == patientId, ct);

            if (s == null)
                return ResponseFactory.Fail<SessionDetailsResponse>("Appointment not found");

            // Specialty يمكن إضافته لاحقاً عبر join مع DoctorProfiles
            var doctorSpecialty = await _context.DoctorProfiles
                .Where(dp => dp.UserId == s.DoctorId)
                .Select(dp => (string)null)   // BaseDoctore يحتوي الـ Specialty — placeholder
                .FirstOrDefaultAsync(ct) ?? "";

            return ResponseFactory.Success(new SessionDetailsResponse
            {
                Id                 = s.Id,
                ScheduledDate      = s.ScheduledDate,
                CreatedAt          = s.CreatedAt,
                DurationMinutes    = s.DurationMinutes,
                Status             = s.Status,
                CancellationReason = s.CancellationReason,
                DoctorId           = s.DoctorId,
                DoctorName         = s.Doctor != null ? $"{s.Doctor.FirstName} {s.Doctor.LastName}" : "Unknown",
                DoctorSpecialty    = doctorSpecialty,
                PatientId          = s.PatientId,
                PatientName        = s.Patient != null ? $"{s.Patient.FirstName} {s.Patient.LastName}" : "Unknown",
                PatientPhone       = s.Patient?.PhoneNumber ?? "",
                ReportsCount       = s.Reports?.Count ?? 0,
                HasProgressTracker = s.ProgressTrackers?.Any() ?? false
            }, "Appointment details retrieved");
        }

        // ── CANCEL confirmed session ────────────────────────────────────────────
        public async Task<BaseResponse<Unit>> CancelAppointmentAsync(
            CancelAppointmentCommand command, CancellationToken ct)
        {
            var session = await _context.Sessions
                .FirstOrDefaultAsync(s => s.Id == command.SessionId && s.PatientId == command.PatientId, ct);

            if (session == null)
                return ResponseFactory.Fail<Unit>("Appointment not found");

            if (session.Status != SessionStatus.Scheduled)
                return ResponseFactory.Fail<Unit>("Only scheduled appointments can be cancelled");

            session.Status             = SessionStatus.Cancelled;
            session.CancellationReason = command.Reason;
            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(Unit.Value, "Appointment cancelled successfully");
        }

        // ── CANCEL pending booking request ──────────────────────────────────────
        public async Task<BaseResponse<Unit>> CancelBookingRequestAsync(
            CancelBookingRequestCommand command, CancellationToken ct)
        {
            var booking = await _context.PublicBookings
                .FirstOrDefaultAsync(b => b.Id == command.BookingId, ct);

            if (booking == null)
                return ResponseFactory.Fail<Unit>("Booking request not found");

            if (booking.Status != Status.Pending)
                return ResponseFactory.Fail<Unit>("Only pending requests can be cancelled");

            booking.Status = Status.Rejected;
            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(Unit.Value, "Booking request cancelled successfully");
        }
    }
}
