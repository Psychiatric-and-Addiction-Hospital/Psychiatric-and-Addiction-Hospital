using Application.Common.Responses;
using Application.DTOS.Request.HR.ApplicationOffer;
using Application.DTOS.Responses.HR.ApplicationOffer;
using MediatR;

namespace Application.Commands.HR.ApplicationOffer
{
    public record UpdateApplicationOfferCommand(UpdateApplicationOfferRequest request)
        : IRequest<BaseResponse<ApplicationOfferResponse>>;

}
