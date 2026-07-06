using Application.Commands.Patient;
using Application.Common.Interfaces.Patient;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Domain.Entites;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.Patient
{
    public class AddSessionNoteService : IAddSessionNote
    {
        private readonly AddIdentityDbContext _context;

        public AddSessionNoteService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<SessionNoteResponse>> AddNoteAsync(AddSessionNoteCommand command, CancellationToken ct)
        {
            var session = await _context.Sessions
                .FirstOrDefaultAsync(s => s.Id == command.SessionId, ct);

            if (session == null)
                return ResponseFactory.Fail<SessionNoteResponse>("Session not found",
                    new List<string> { "No session exists with the given sessionId." });

            var doctor = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == command.DoctorId, ct);

            if (doctor == null)
                return ResponseFactory.Fail<SessionNoteResponse>("Doctor not found",
                    new List<string> { "No user exists with the given doctorId." });

            var report = new Report
            {
                DoctorId = command.DoctorId,
                PatientId = command.PatientId,
                SessionId = command.SessionId,
                Diagnosis = command.Diagnosis,
                Notes = command.Notes,
                TreatmentPlan = command.TreatmentPlan,
                ConditionRate = command.ConditionRate,
                AttachmentUrl = command.AttachmentUrl,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Reports.AddAsync(report, ct);
            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(new SessionNoteResponse
            {
                Id = report.Id,
                SessionId = report.SessionId,
                DoctorId = report.DoctorId,
                DoctorName = $"{doctor.FirstName} {doctor.LastName}",
                Diagnosis = report.Diagnosis,
                Notes = report.Notes,
                TreatmentPlan = report.TreatmentPlan,
                ConditionRate = report.ConditionRate,
                AttachmentUrl = report.AttachmentUrl,
                CreatedAt = report.CreatedAt
            }, "Session note added successfully");
        }
    }
}
