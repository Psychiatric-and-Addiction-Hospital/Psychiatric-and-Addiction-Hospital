using Application.Common.Responses;
using Application.DTOS.Request.Doctor;
using Application.DTOS.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Doctores.Schedule
{
    public interface IGetDoctorSchedules
    {
        Task<BaseResponse<PagedResponse<ScheduleResponse>>> GetDoctorSchedulesAsync(GetDoctorScheduleListRequest request,CancellationToken ct);
    }
}
