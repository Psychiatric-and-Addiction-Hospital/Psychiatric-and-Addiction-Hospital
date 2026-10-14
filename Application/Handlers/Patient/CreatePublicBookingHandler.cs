using Application.Commands.Patient;
using Application.Common.Interfaces.Patient;
using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Patient
{
    public class CreatePublicBookingHandler : IRequestHandler<CreatePublicBookingCommand, BaseResponse<PublicBookingResponse>>
    {
        private readonly ICreatePublicBooking _Booking;
        public CreatePublicBookingHandler(ICreatePublicBooking Booking)
        {
            _Booking = Booking;
        }
        public async Task<BaseResponse<PublicBookingResponse>> Handle(CreatePublicBookingCommand request, CancellationToken ct)
        {
            return await _Booking.CreatePublicBooking(request.request, ct);
        }
    }
}
