using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationOffer;
using MediatR;
using System;

namespace Application.Queries.HR.ApplicationOffer
{
    public record GetApplicationOfferByIdQuery(Guid Id) : IRequest<BaseResponse<ApplicationOfferResponse>>;

}
