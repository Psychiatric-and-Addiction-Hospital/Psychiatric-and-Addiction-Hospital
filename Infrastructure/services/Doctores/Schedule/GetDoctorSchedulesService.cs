using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Doctores.Schedule;
using Application.Common.Responses;
using Application.DTOS.Request.Doctor;
using Application.DTOS.Responses;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Application.Common.Extensions;

namespace Infrastructure.services.Doctores.Schedule
{
    public class GetDoctorSchedulesService : IGetDoctorSchedules
    {
        private readonly AddIdentityDbContext _Context;
        private readonly ICurrentUser _currentUser;
        public GetDoctorSchedulesService(AddIdentityDbContext context, ICurrentUser currentUser)
        {
            _Context = context;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<PagedResponse<ScheduleResponse>>> GetDoctorSchedulesAsync(GetDoctorScheduleListRequest request,CancellationToken ct)
        {
            if (!_currentUser.IsAuthenticated)
                return ResponseFactory.Fail<PagedResponse<ScheduleResponse>>("User must be authenticated.");

            var userId = _currentUser.UserId;

            if (string.IsNullOrWhiteSpace(userId))
                return ResponseFactory.Fail<PagedResponse<ScheduleResponse>>("Authenticated user must have a valid user ID.");


            var doctor = await _Context.DoctorProfiles.FirstOrDefaultAsync(d => d.Employee.AppUserId == userId, ct);
            if (doctor is null)
                return ResponseFactory.Fail<PagedResponse<ScheduleResponse>>("Doctor not found.");

            var query = _Context.DoctorSchedules
                .AsNoTracking()
                .Where(s => s.DoctorProfileId == doctor.Id)
                .Include(x => x.DoctorProfile)
                     .ThenInclude(x => x.Employee)
                          .ThenInclude(x => x.AppUser)
                .AsQueryable();

            var responseQuery = query.Select(s => new ScheduleResponse
            {
                Id = s.Id,
                DoctorId = s.DoctorProfileId,
                Date = s.Date,
                Time = s.Time,
                IsBooked = s.IsBooked
            });

            var pagedResult = await responseQuery.ToPagedResponseAsync(
               request.PageNumber,
               request.PageSize,
               ct);

            return ResponseFactory.Success(pagedResult, "Schedules retrieved successfully.");


        }
    }
}