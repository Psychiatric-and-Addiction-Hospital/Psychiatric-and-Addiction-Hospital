using Application.Common.Interfaces.Doctores.Schedule;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Application.Queries.Doctor.DoctorSchedule;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Doctores.Schedule
{
    public class GetDoctorSchedulesHandler : IRequestHandler<GetDoctorSchedulesQuery, BaseResponse<PagedResponse<ScheduleResponse>>>
    {
        private readonly IGetDoctorSchedules _service;
        public GetDoctorSchedulesHandler(IGetDoctorSchedules service)
        {
            _service = service;
        }
        public async Task<BaseResponse<PagedResponse<ScheduleResponse>>> Handle(GetDoctorSchedulesQuery request, CancellationToken ct)
        {
            return await _service.GetDoctorSchedulesAsync(request.request, ct);
        }
    }
}
