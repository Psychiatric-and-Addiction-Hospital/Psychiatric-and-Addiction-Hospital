using Application.Common.Responses;
using Application.DTOS.Request.Doctor;
using Application.DTOS.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Doctores.Schedule
{
    public interface ICreateDoctorSchedule
    {
        Task<BaseResponse<ScheduleResponse>> CreateDoctorSchedule(CreateDoctorRequest request, CancellationToken ct);
    }
}
