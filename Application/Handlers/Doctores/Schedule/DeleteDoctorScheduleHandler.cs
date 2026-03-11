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
    public class DeleteDoctorScheduleHandler : IRequestHandler<DeleteDoctorScheduleCommand, BaseResponse<ScheduleResponse>>
    {
        private readonly IDeleteDoctorSchedule _deleteSchedule;
        public DeleteDoctorScheduleHandler(IDeleteDoctorSchedule deleteSchedule)
        {
            _deleteSchedule = deleteSchedule;
        }
        public async Task<BaseResponse<ScheduleResponse>> Handle(DeleteDoctorScheduleCommand request, CancellationToken ct)
        {
            return await _deleteSchedule.DeleteDoctorScheduleAsync(request.Id,ct);
        }
    }
}
