using Application.Common.Interfaces.Patient;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.services.Patient
{
    public class GetSessionDetailsService : IGetSessionDetails
    {
        private readonly AddIdentityDbContext _context;

        public GetSessionDetailsService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<SessionDetailResponse>> GetDetailsAsync(Guid sessionId, CancellationToken ct)
        {
            var session = await _context.Sessions
                .Include(s => s.Doctor)
                .Include(s => s.Patient)
                .Include(s => s.Reports)
                    .ThenInclude(r => r.Doctor)
                .FirstOrDefaultAsync(s => s.Id == sessionId, ct);

            if (session == null)
                return ResponseFactory.Fail<SessionDetailResponse>("Session not found",
                    new List<string> { "No session exists with the given sessionId." });

            var notes = session.Reports
                .OrderBy(r => r.CreatedAt)
                .Select(r => new SessionNoteResponse
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
                }).ToList();

            return ResponseFactory.Success(new SessionDetailResponse
            {
                Id = session.Id,
                ScheduledDate = session.ScheduledDate,
                DurationMinutes = session.DurationMinutes,
                SessionType = session.SessionType.ToString(),
                Status = session.Status.ToString(),
                CancellationReason = session.CancellationReason,
                DoctorId = session.DoctorId,
                DoctorName = session.Doctor != null
                    ? $"{session.Doctor.FirstName} {session.Doctor.LastName}"
                    : "Unknown",
                PatientId = session.PatientId,
                PatientName = session.Patient != null
                    ? $"{session.Patient.FirstName} {session.Patient.LastName}"
                    : "Unknown",
                Notes = notes
            }, "Session details retrieved successfully");
        }
    }
}
