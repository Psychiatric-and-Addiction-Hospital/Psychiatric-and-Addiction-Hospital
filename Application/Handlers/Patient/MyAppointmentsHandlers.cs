using Application.Commands.Patient;
using Application.Common.Interfaces.Patient;
using Application.Common.Responses;
using Application.DTOS.Responses.Session;
using Application.DTOs.Responses.Sessions;
using Application.Queries.Patient;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Patient
{
    // GET /api/MyAppointments
    public class GetMyAppointmentsHandler
        : IRequestHandler<GetMyAppointmentsQuery, BaseResponse<List<SessionSummaryResponse>>>
    {
        private readonly IMyAppointmentsService _service;
        public GetMyAppointmentsHandler(IMyAppointmentsService service) => _service = service;

        public Task<BaseResponse<List<SessionSummaryResponse>>> Handle(GetMyAppointmentsQuery request, CancellationToken ct)
            => _service.GetAppointmentsAsync(request.PatientId, request.Status, ct);
    }

    // GET /api/MyAppointments/{id}
    public class GetMyAppointmentDetailsHandler
        : IRequestHandler<GetMyAppointmentDetailsQuery, BaseResponse<SessionDetailsResponse>>
    {
        private readonly IMyAppointmentsService _service;
        public GetMyAppointmentDetailsHandler(IMyAppointmentsService service) => _service = service;

        public Task<BaseResponse<SessionDetailsResponse>> Handle(GetMyAppointmentDetailsQuery request, CancellationToken ct)
            => _service.GetAppointmentDetailsAsync(request.SessionId, request.PatientId, ct);
    }

    // POST /api/MyAppointments/{id}/cancel
    public class CancelAppointmentHandler
        : IRequestHandler<CancelAppointmentCommand, BaseResponse<Unit>>
    {
        private readonly IMyAppointmentsService _service;
        public CancelAppointmentHandler(IMyAppointmentsService service) => _service = service;

        public Task<BaseResponse<Unit>> Handle(CancelAppointmentCommand request, CancellationToken ct)
            => _service.CancelAppointmentAsync(request, ct);
    }

    // POST /api/MyAppointments/{id}/cancel-request
    public class CancelBookingRequestHandler
        : IRequestHandler<CancelBookingRequestCommand, BaseResponse<Unit>>
    {
        private readonly IMyAppointmentsService _service;
        public CancelBookingRequestHandler(IMyAppointmentsService service) => _service = service;

        public Task<BaseResponse<Unit>> Handle(CancelBookingRequestCommand request, CancellationToken ct)
            => _service.CancelBookingRequestAsync(request, ct);
    }
}
