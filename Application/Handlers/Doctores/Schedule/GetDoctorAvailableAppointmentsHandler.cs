using Application.Common.Interfaces.Doctores.Schedule;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Application.Queries.Doctor.DoctorSchedule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Doctores.Schedule
{
    public class GetDoctorAvailableAppointmentsHandler
        : IRequestHandler<GetDoctorAvailableAppointmentsQuery, BaseResponse<List<DoctorAppointmentResponse>>>
    {
        private readonly IGetDoctorAvailableAppointments _getDoctorAvailableAppointments;
        public GetDoctorAvailableAppointmentsHandler(IGetDoctorAvailableAppointments getDoctorAvailableAppointments)
        {
            _getDoctorAvailableAppointments = getDoctorAvailableAppointments;
        }
        public async Task<BaseResponse<List<DoctorAppointmentResponse>>> Handle(GetDoctorAvailableAppointmentsQuery request, CancellationToken cancellationToken)
        {
          return await _getDoctorAvailableAppointments.GetAvailableAsync(request.DoctorId);
        }
    }
}
