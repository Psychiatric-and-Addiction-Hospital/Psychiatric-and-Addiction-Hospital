using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;
using System;
using System.Collections.Generic;


namespace Application.Queries.Doctor.DoctorSchedule
{
    public record GetDoctorAvailableAppointmentsQuery(Guid DoctorId) 
        : IRequest<BaseResponse<List<DoctorAppointmentResponse>>>;

}
