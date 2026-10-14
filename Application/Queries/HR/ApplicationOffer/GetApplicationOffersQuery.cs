using Application.Common.Responses;
using Application.DTOS.Request.HR.ApplicationOffer;
using Application.DTOS.Responses.HR.ApplicationOffer;
using MediatR;

namespace Application.Queries.HR.ApplicationOffer
{
    public record GetApplicationOffersQuery(ApplicationOfferListRequest request) 
        : IRequest<BaseResponse<PagedResponse<ApplicationOfferResponse>>>;
}
