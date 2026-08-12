using Application.Common.Responses;
using Application.DTOS.Request.HR.ApplicationOffer;
using System;
using System.Threading;
using System.Threading.Tasks;
using Offer = Domain.Entites.HR.Recruitment.ApplicationOffer; 

namespace Application.Common.Interfaces.HR.ApplicationOffer
{
    public interface IApplicationOfferValidation
    {
        Task<BaseResponse<bool>> ValidateCreateAsync(CreateApplicationOfferRequest request, CancellationToken ct);

        Task<BaseResponse<Offer>> ValidateUpdateAsync(UpdateApplicationOfferRequest request, CancellationToken ct);

        Task<BaseResponse<Offer>> ValidateAcceptAsync(Guid offerId, CancellationToken ct);

        Task<BaseResponse<Offer>> ValidateRejectAsync(Guid offerId, CancellationToken ct);

        Task<BaseResponse<Offer>> ValidateDeleteAsync(Guid offerId, CancellationToken ct);
    }
}
