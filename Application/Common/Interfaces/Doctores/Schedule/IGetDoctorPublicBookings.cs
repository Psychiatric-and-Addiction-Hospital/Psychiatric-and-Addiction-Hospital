using Application.Common.Responses;
using Application.DTOS.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Doctores.Schedule
{
    public interface IGetDoctorPublicBookings
    {
        Task<BaseResponse<List<PublicBookingResponse>>> GetBookings(CancellationToken ct);
    }
}
