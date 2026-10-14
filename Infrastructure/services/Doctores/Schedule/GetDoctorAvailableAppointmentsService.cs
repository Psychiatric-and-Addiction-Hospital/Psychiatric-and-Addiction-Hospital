using Application.Common.Interfaces.Doctores.Schedule;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.Doctores.Schedule
{
    public class GetDoctorAvailableAppointmentsService : IGetDoctorAvailableAppointments
    {
        private readonly AddIdentityDbContext _Context;
        public GetDoctorAvailableAppointmentsService(AddIdentityDbContext context)
        {
            _Context = context;
        }

        public async Task<BaseResponse<List<DoctorAppointmentResponse>>> GetAvailableAsync(Guid doctorId,CancellationToken ct)
        {
            var now = DateTime.Now;
            var today = DateOnly.FromDateTime(now);
            var currentTime = TimeOnly.FromDateTime(now);

            var appointments = await _Context.DoctorSchedules
                 .AsNoTracking()
                 .Where(x =>
                     x.DoctorProfileId == doctorId &&
                     !x.IsBooked &&
                     (x.Date > today || (x.Date == today && x.Time >= currentTime)))
                 .OrderBy(x => x.Date)
                 .ThenBy(x => x.Time)
                 .Select(x => new DoctorAppointmentResponse
                 {
                     AppointmentId = x.Id,
                     Date = x.Date,
                     Time = x.Time
                 })
                 .ToListAsync(ct);

            if ( appointments.Count == 0)
            {
                return ResponseFactory.Fail<List<DoctorAppointmentResponse>>("No available appointments",
                    new List<string> { "There are no available appointments for the specified doctor." });
            }
            return ResponseFactory.Success(appointments, "Available appointments retrieved successfully");
        }
    }
}
