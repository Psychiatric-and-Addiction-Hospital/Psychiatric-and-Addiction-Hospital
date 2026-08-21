using Application.Common.Interfaces.Patient;
using Application.Common.Responses;
using Application.DTOS.Responses;
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
    public class GetPatientDashboardService : IGetPatientDashboard
    {
        private readonly AddIdentityDbContext _context;

        public GetPatientDashboardService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<PatientDashboardResponse>> GetDashboardAsync(string patientId, CancellationToken ct)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            // Next upcoming appointment (Scheduled, future)
            var nextSession = await _context.Sessions
                .Include(s => s.Doctor)
                .Where(s => s.PatientId == patientId
                         && s.Status == SessionStatus.Scheduled
                         && s.ScheduledDate >= today)
                .OrderBy(s => s.ScheduledDate)
                .FirstOrDefaultAsync(ct);

            // Recent 5 sessions (any status)
            var recentSessions = await _context.Sessions
                .Include(s => s.Doctor)
                .Where(s => s.PatientId == patientId)
                .OrderByDescending(s => s.ScheduledDate)
                .Take(5)
                .ToListAsync(ct);

            // Recent 5 notes/reports for this patient
            var recentReports = await _context.Reports
                .Include(r => r.Doctor)
                .Where(r => r.PatientId == patientId)
                .OrderByDescending(r => r.CreatedAt)
                .Take(5)
                .ToListAsync(ct);

            var dashboard = new PatientDashboardResponse
            {
                NextAppointment = nextSession == null ? null : new SessionSummaryResponse
                {
                    Id = nextSession.Id,
                    ScheduledDate = nextSession.ScheduledDate,
                    DurationMinutes = nextSession.DurationMinutes,
                    SessionType = nextSession.SessionType.ToString(),
                    Status = nextSession.Status.ToString(),
                    DoctorId = nextSession.DoctorId,
                    DoctorName = nextSession.Doctor != null
                        ? $"{nextSession.Doctor.FirstName} {nextSession.Doctor.LastName}"
                        : "Unknown"
                },
                RecentSessions = recentSessions.Select(s => new SessionSummaryResponse
                {
                    Id = s.Id,
                    ScheduledDate = s.ScheduledDate,
                    DurationMinutes = s.DurationMinutes,
                    SessionType = s.SessionType.ToString(),
                    Status = s.Status.ToString(),
                    DoctorId = s.DoctorId,
                    DoctorName = s.Doctor != null
                        ? $"{s.Doctor.FirstName} {s.Doctor.LastName}"
                        : "Unknown"
                }).ToList(),
                RecentNotes = recentReports.Select(r => new SessionNoteResponse
                {
                    Id = r.Id,
                    SessionId = r.SessionId,
                    DoctorId = r.DoctorId,
                    DoctorName = r.Doctor != null
                        ? $"{r.Doctor.FirstName} {r.Doctor.LastName}"
                        : "Unknown",
                    Diagnosis = r.Diagnosis,
                    Notes = r.Notes,
                    TreatmentPlan = r.TreatmentPlan,
                    ConditionRate = r.ConditionRate,
                    AttachmentUrl = r.AttachmentUrl,
                    CreatedAt = r.CreatedAt
                }).ToList()
            };

            return ResponseFactory.Success(dashboard, "Patient dashboard retrieved successfully");
        }
    }
}
