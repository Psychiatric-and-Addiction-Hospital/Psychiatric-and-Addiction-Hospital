using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Doctores.Schedule;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.Doctores.Schedule
{
    public class DeleteDoctorScheduleService : IDeleteDoctorSchedule
    {
        private readonly AddIdentityDbContext _Context;
        private readonly ICurrentUser _currentUser;
        public DeleteDoctorScheduleService(AddIdentityDbContext context, ICurrentUser currentUser)
        {
            _Context = context;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<ScheduleResponse>> DeleteDoctorScheduleAsync(Guid Id, CancellationToken ct)
        {
            if (!_currentUser.IsAuthenticated)
                return ResponseFactory.Fail<ScheduleResponse>("User must be authenticated.");

            var userId = _currentUser.UserId;

            if (string.IsNullOrWhiteSpace(userId))
                return ResponseFactory.Fail<ScheduleResponse>("Authenticated user must have a valid user ID.");

            var slot = await _Context.DoctorSchedules
                 .FirstOrDefaultAsync(s => s.Id == Id, ct);

            if (slot is null)
                return ResponseFactory.Fail<ScheduleResponse>("Schedule slot not found.");

            var doctorOwnsSlot = await _Context.DoctorProfiles
                .AsNoTracking()
                .AnyAsync(d => d.Id == slot.DoctorProfileId && d.Employee.AppUserId == userId, ct);

            if (!doctorOwnsSlot)
                return ResponseFactory.Fail<ScheduleResponse>("You are not authorized to delete this schedule slot.");

            if (slot.IsBooked)
                return ResponseFactory.Fail<ScheduleResponse>("Cannot delete a schedule slot that is already booked.");

            _Context.DoctorSchedules.Remove(slot);
            await _Context.SaveChangesAsync(ct);

            return ResponseFactory.Success(new ScheduleResponse
            {
                Id = slot.Id,
                DoctorId = slot.DoctorProfileId,
                Date = slot.Date,
                Time = slot.Time,
                IsBooked = slot.IsBooked
            }, "Schedule slot deleted successfully.");
        }
    }
}
