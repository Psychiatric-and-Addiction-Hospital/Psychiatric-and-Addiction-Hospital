using Application.Common.Responses;
using Application.DTOS.Responses.booking;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Doctores.Booking
{
    public interface IApproveBooking
    {
        Task<BaseResponse<ApproveBookingResponse>> ApproveBookingAsync(Guid bookingId, CancellationToken ct);
    }
}
