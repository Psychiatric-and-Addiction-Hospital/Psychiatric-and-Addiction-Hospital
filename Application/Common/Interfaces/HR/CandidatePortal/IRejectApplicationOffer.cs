using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationOffer;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.CandidatePortal
{
    public interface IRejectApplicationOffer
    {
        Task<BaseResponse<ApplicationOfferResponse>> RejectAsync(Guid OfferId, CancellationToken ct);
    }
}
