using Application.Common.Interfaces.Patient;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.services.Patient
{
    public class GetPatientSessionsService : IGetPatientSessions
    {
        private readonly AddIdentityDbContext _context;

        public GetPatientSessionsService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<List<SessionSummaryResponse>>> GetSessionsAsync(string patientId, CancellationToken ct)
        {
            var sessions = await _context.Sessions
                .Include(s => s.Doctor)
                .Where(s => s.PatientId == patientId)
                .OrderByDescending(s => s.ScheduledDate)
                .ToListAsync(ct);

            var result = sessions.Select(s => new SessionSummaryResponse
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
            }).ToList();

            return ResponseFactory.Success(result, $"{result.Count} session(s) retrieved");
        }
    }
}
