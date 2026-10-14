using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationOffer;
using MediatR;
using System;


namespace Application.Commands.HR.ApplicationOffer
{
    public record AcceptApplicationOfferCommand(Guid OfferId) : IRequest<BaseResponse<ApplicationOfferResponse>>;

}
