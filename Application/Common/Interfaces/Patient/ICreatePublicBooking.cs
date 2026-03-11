using Application.Common.Responses;
using Application.DTOS.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Patient
{
    public interface ICreatePublicBooking
    {
        Task<BaseResponse<PublicBookingResponse>> CreatePublicBooking(string fullName, string phoneNumber,
         string email, Guid doctorId, Guid ScheduleId, string notes,CancellationToken ct);
    }
}
