using Application.Common.Interfaces.Doctores.Schedule;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Application.Queries.Doctor.DoctorSchedule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Doctores.Schedule
{
    public class GetDoctorSchedulesHandler : IRequestHandler<GetDoctorSchedulesQuery, BaseResponse<List<ScheduleResponse>>>
    {
        private readonly IGetDoctorSchedules _getSchedules;
        public GetDoctorSchedulesHandler(IGetDoctorSchedules getSchedules)
        {
            _getSchedules = getSchedules;
        }
        public async Task<BaseResponse<List<ScheduleResponse>>> Handle(GetDoctorSchedulesQuery request, CancellationToken ct)
        {
            return await _getSchedules.GetDoctorSchedulesAsync(request.DoctorId,ct);
        }
    }
}
