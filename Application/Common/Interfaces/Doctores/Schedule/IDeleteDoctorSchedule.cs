using Application.Common.Responses;
using Application.DTOS.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Doctores.Schedule
{
    public interface IDeleteDoctorSchedule
    {
        Task<BaseResponse<ScheduleResponse>> DeleteDoctorScheduleAsync(Guid Id, CancellationToken ct);
    }
}
