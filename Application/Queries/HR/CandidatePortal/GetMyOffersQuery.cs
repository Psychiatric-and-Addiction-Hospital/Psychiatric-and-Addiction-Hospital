using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationOffer;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.HR.CandidatePortal
{
    public record GetMyOffersQuery() : IRequest<BaseResponse<List<ApplicationOfferResponse>>>;
}
