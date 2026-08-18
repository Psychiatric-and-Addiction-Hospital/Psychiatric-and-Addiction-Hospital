using Application.Common.Interfaces.HR.CandidatePortal;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationOffer;
using Application.Queries.HR.CandidatePortal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.CandidatePortal
{
    public class GetMyOffersHandler : IRequestHandler<GetMyOffersQuery, BaseResponse<List<ApplicationOfferResponse>>>
    {
        private readonly IGetMyOffers _service;

        public GetMyOffersHandler(IGetMyOffers service)
        {
            _service = service;
        }
        public async Task<BaseResponse<List<ApplicationOfferResponse>>> Handle(GetMyOffersQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAsync(cancellationToken);
        }
    }
}
