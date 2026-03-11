using Application.Common.Interfaces.Doctores.Schedule;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.Doctores.Schedule
{
    public class GetDoctorSchedulesService : IGetDoctorSchedules
    {
        private readonly AddIdentityDbContext _Context;
        public GetDoctorSchedulesService(AddIdentityDbContext context)
        {
            _Context = context;
        }

        public async Task<BaseResponse<List<ScheduleResponse>>> GetDoctorSchedulesAsync(Guid doctorId, CancellationToken ct)
        {
            var Schedule = await _Context.DoctorSchedules.Where(s => s.DoctorProfileId == doctorId)
                .Select(s => new ScheduleResponse
                {
                    Id = s.Id,
                    DoctorId = s.DoctorProfileId,
                    Date = s.Date,
                    Time = s.Time,
                    IsBooked = s.IsBooked
                }).ToListAsync(ct);

            if (Schedule == null || Schedule.Count == 0)
            {
                return ResponseFactory.Fail<List<ScheduleResponse>>
                    ("No schedules found for the specified doctor.",
                    new List<string> { "No schedules available." });
            }

            return ResponseFactory.Success<List<ScheduleResponse>>(Schedule, "Schedules retrieved successfully.");
            
            
        }
    }
}