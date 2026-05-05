using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Domain.Enums;
using MediatR;
using System;

namespace Application.Commands.HR.ApplicationOffer
{
    public record UpdateApplicationOfferStatusCommand(Guid OfferId, OfferStatues Status) : 
        IRequest<BaseResponse<ApplicationOfferResponse>>;

}
