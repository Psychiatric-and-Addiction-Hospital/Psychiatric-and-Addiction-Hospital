using Application.Common.Responses;
using Application.DTOS.Responses.booking;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Doctores.Booking
{
    public interface IRejectBooking
    {
        Task<BaseResponse<RejectBookingResponse>> RejectBookingAsync(Guid bookingId, string rejectionReason, CancellationToken ct);
    }
}
