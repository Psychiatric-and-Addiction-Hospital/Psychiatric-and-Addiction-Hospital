using Application.Common.Interfaces.Doctores.Schedule;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Domain.Entites.DoctorsModule;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.Doctores.Schedule
{
    public class CreateDoctorScheduleService: ICreateDoctorSchedule
    {
        private readonly AddIdentityDbContext _Context;
        public CreateDoctorScheduleService(AddIdentityDbContext context)
        {
            _Context = context;
        }

        public async Task<BaseResponse<ScheduleResponse>> CreateDoctorSchedule(Guid doctorId, DateTime date, string time, CancellationToken ct)
        {
            var doctor = await _Context.DoctorProfiles.FirstOrDefaultAsync(d => d.Id == doctorId, ct);
            if(doctor==null)
                return ResponseFactory.Fail<ScheduleResponse>("Doctor not found");
            var slot = new DoctorSchedule
            {
                DoctorProfileId = doctorId,
                Date = date,
                Time = time
            };
            await _Context.DoctorSchedules.AddAsync(slot, ct);
            await _Context.SaveChangesAsync(ct);
            return  ResponseFactory.Success(new ScheduleResponse
            {
                Id = slot.Id,
                DoctorId = doctorId,
                Date = date,
                Time = time,
                IsBooked = slot.IsBooked
            },"CreateDoctorSchedule" );
        }
    }
}
