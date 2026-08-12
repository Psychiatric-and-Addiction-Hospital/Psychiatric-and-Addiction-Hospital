using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationOffer;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.ApplicationOffer
{
    public interface IAcceptApplicationOffer
    {
        Task<BaseResponse<ApplicationOfferResponse>> AcceptAsync(Guid OfferId, CancellationToken ct);
    }
}
