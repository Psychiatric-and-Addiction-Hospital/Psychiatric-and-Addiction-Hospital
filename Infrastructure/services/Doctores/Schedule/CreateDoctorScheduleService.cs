using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Doctores.Schedule;
using Application.Common.Responses;
using Application.DTOS.Request.Doctor;
using Application.DTOS.Responses;
using Application.DTOS.Responses.HR.Candidate;
using Domain.Entites.DoctorsModule;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.Doctores.Schedule
{
    public class CreateDoctorScheduleService : ICreateDoctorSchedule
    {
        private readonly AddIdentityDbContext _context;
        private readonly ICurrentUser _currentUser;
        public CreateDoctorScheduleService(AddIdentityDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<ScheduleResponse>> CreateDoctorSchedule(CreateDoctorRequest request, CancellationToken ct)
        {
            if (!_currentUser.IsAuthenticated)
                return ResponseFactory.Fail<ScheduleResponse>("User must be authenticated.");

            var userId = _currentUser.UserId;

            if (string.IsNullOrWhiteSpace(userId))
                return ResponseFactory.Fail<ScheduleResponse>("Authenticated user must have a valid user ID.");


            var doctor = await _context.DoctorProfiles.FirstOrDefaultAsync(d => d.Employee.AppUserId == userId, ct);
            if (doctor is null)
                return ResponseFactory.Fail<ScheduleResponse>("Doctor not found");

            var slotExists = await _context.DoctorSchedules
                .AsNoTracking()
                .AnyAsync(s =>
              s.DoctorProfileId == doctor.Id &&
              s.Date == request.Date &&
              s.Time == request.Time, ct);

            if (slotExists)
                return ResponseFactory.Fail<ScheduleResponse>("This doctor already has a schedule slot at the same date and time.");

            var slot = new DoctorSchedule
            {
                DoctorProfileId = doctor.Id,
                Date = request.Date,
                Time = request.Time
            };
            await _context.DoctorSchedules.AddAsync(slot, ct);
            await _context.SaveChangesAsync(ct);
            return ResponseFactory.Success(new ScheduleResponse
            {
                Id = slot.Id,
                DoctorId = doctor.Id,
                Date = request.Date,
                Time = request.Time,
                IsBooked = slot.IsBooked
            }, "CreateDoctorSchedule");
        }
    }
}
