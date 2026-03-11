using Application.Common.Interfaces.Doctores.Schedule;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Infrastructure.Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.Doctores.Schedule
{
    public class DeleteDoctorScheduleService: IDeleteDoctorSchedule
    {
        private readonly AddIdentityDbContext _Context;
        public DeleteDoctorScheduleService(AddIdentityDbContext context) 
        {
            _Context = context;
        }

        public async Task<BaseResponse<ScheduleResponse>> DeleteDoctorScheduleAsync(Guid Id, CancellationToken ct)
        {
            var schedule = await _Context.DoctorSchedules.FindAsync(Id);
            if (schedule == null) 
            {
                return ResponseFactory.Fail<ScheduleResponse>("Schedule not found.");
                //return new BaseResponse<ScheduleResponse>
                //{

                //    Success = false,
                //    Message = "Schedule not found",
                //    Data = null,
                //    Errors = new List<string> { "Schedule with the provided ID does not exist." }
                //};
            }
             _Context.DoctorSchedules.Remove(schedule);
            await _Context.SaveChangesAsync(ct);
            return ResponseFactory.Success<ScheduleResponse>(null, "Schedule Deleted Successfully");
            //return new BaseResponse<ScheduleResponse>
            //{
            //    Success = true,
            //    Message = "Schedule Deleted Successfully",
            //    Data = null,
            //    Errors = null
            //
        }
    }
}
