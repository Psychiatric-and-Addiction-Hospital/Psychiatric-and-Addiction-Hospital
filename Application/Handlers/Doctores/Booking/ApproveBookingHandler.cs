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
    public class ApproveBookingHandler : IRequestHandler<ApproveBookingCommand, BaseResponse<ApproveBookingResponse>>
    {
        private readonly IApproveBooking _approveBooking;

        public ApproveBookingHandler(IApproveBooking approveBooking)
        {
            _approveBooking = approveBooking;
        }

        public async Task<BaseResponse<ApproveBookingResponse>> Handle(ApproveBookingCommand request, CancellationToken ct)
        {
            return await _approveBooking.ApproveBookingAsync(request.BookingId, ct);
        }
    }
}
