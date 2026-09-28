using Application.Common.Responses;
using Application.DTOS.Responses.Session;
using Application.DTOs.Responses.Sessions;
using MediatR;
using System;
using System.Collections.Generic;

namespace Application.Queries.Patient
{
    // GET /api/MyAppointments?status=All|Upcoming|Completed|Cancelled
    public record GetMyAppointmentsQuery(string PatientId, string Status = "All")
        : IRequest<BaseResponse<List<SessionSummaryResponse>>>;

    // GET /api/MyAppointments/{id}
    public record GetMyAppointmentDetailsQuery(Guid SessionId, string PatientId)
        : IRequest<BaseResponse<SessionDetailsResponse>>;
}
