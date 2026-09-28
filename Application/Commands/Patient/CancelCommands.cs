using Application.Common.Responses;
using MediatR;
using System;

namespace Application.Commands.Patient
{
    // POST /api/MyAppointments/{id}/cancel  ← إلغاء موعد مؤكد (Scheduled)
    public record CancelAppointmentCommand(Guid SessionId, string PatientId, string Reason)
        : IRequest<BaseResponse<Unit>>;

    // POST /api/MyAppointments/{id}/cancel-request  ← إلغاء طلب Pending في PublicBooking
    public record CancelBookingRequestCommand(Guid BookingId, string PatientId)
        : IRequest<BaseResponse<Unit>>;
}
