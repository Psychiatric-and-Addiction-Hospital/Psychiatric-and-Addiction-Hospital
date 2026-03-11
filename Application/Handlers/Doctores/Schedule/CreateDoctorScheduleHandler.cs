using Application.Commands.Doctores.Schedule;
using Application.Common.Interfaces.Doctores.Schedule;
using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Doctores.Schedule
{
    public class CreateDoctorScheduleHandler : IRequestHandler<CreateDoctorScheduleCommand, BaseResponse<ScheduleResponse>>
    {
        private readonly ICreateDoctorSchedule _Schedule;
        public CreateDoctorScheduleHandler(ICreateDoctorSchedule Schedule)
        {
            _Schedule = Schedule;
        }

        public async Task<BaseResponse<ScheduleResponse>> Handle(CreateDoctorScheduleCommand request, CancellationToken ct)
        {
            return await _Schedule.CreateDoctorSchedule(request.DoctorId, request.Date, request.Time, ct);
        }
    }
}
