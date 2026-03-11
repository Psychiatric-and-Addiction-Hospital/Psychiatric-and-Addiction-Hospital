using Application.Common.Responses;
using Application.DTOS.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Doctores.Schedule
{
    public interface IGetDoctorPublicBookings
    {
        Task<BaseResponse<List<PublicBookingResponse>>>GetBookings(Guid doctorId, CancellationToken ct);
    }
}
