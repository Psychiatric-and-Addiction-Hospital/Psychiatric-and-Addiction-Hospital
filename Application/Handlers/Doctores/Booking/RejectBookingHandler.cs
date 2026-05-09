using Application.Commands.Doctores.Booking;
using Application.Common.Interfaces.Doctores.Booking;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Application.DTOS.Responses.booking;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Application.Handlers.Doctores.Booking
{
    public class RejectBookingHandler : IRequestHandler<RejectBookingCommand, BaseResponse<RejectBookingResponse>>
    {
        private readonly IRejectBooking _rejectBooking;

        public RejectBookingHandler(IRejectBooking rejectBooking)
        {
            _rejectBooking = rejectBooking;
        }

        public async Task<BaseResponse<RejectBookingResponse>> Handle(RejectBookingCommand request, CancellationToken ct)
        {
            return await _rejectBooking.RejectBookingAsync(request.BookingId, request.RejectionReason, ct);
        }
    }
}
