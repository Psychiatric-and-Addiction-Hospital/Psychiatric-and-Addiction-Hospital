using Application.Common.Responses;
using Application.DTOS.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Doctores.Schedule
{
    public interface ICreateDoctorSchedule
    {
        Task<BaseResponse<ScheduleResponse>> CreateDoctorSchedule(Guid doctorId, DateTime date, string time,CancellationToken ct);
    }
}
