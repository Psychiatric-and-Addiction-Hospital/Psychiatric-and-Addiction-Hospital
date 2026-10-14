using Application.Common.Responses;
using Application.DTOS.Request.Patient;
using Application.DTOS.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Patient
{
    public interface ICreatePublicBooking
    {
        Task<BaseResponse<PublicBookingResponse>> CreatePublicBooking(CreatePublicBookingRequest rquest, CancellationToken ct);
    }
}
