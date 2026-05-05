using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Domain.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.ApplicationOffer
{
    public interface IUpdateApplicationOfferStatus
    {
        Task<BaseResponse<ApplicationOfferResponse>> UpdateApplicationOfferStatusAsync(
            Guid OfferId, OfferStatues Status,CancellationToken ct);
    }
}
