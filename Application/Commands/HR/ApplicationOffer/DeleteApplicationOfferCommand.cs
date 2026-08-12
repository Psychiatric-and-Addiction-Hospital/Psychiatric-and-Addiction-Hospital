using Application.Common.Responses;
using MediatR;
using System;

namespace Application.Commands.HR.ApplicationOffer
{
    public record DeleteApplicationOfferCommand(Guid OfferId) : IRequest<BaseResponse<bool>>;
}
