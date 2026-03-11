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
    public class GetDoctorPublicBookingsHandler
        : IRequestHandler<GetDoctorPublicBookingsQuery, BaseResponse<List<PublicBookingResponse>>>
    {
        private readonly IGetDoctorPublicBookings _service;

        public GetDoctorPublicBookingsHandler(IGetDoctorPublicBookings service)
        {
            _service = service;
        }

        public async Task<BaseResponse<List<PublicBookingResponse>>> Handle(
            GetDoctorPublicBookingsQuery request,
            CancellationToken ct)
        {
            return await _service.GetBookings(request.DoctorId, ct);
        }
    }
}
