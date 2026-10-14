using Application.Common.Interfaces.HR.ApplicationOffer;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationOffer;
using Application.Queries.HR.ApplicationOffer;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.ApplicationOffer
{
    public class GetApplicationOffersHandler : IRequestHandler<GetApplicationOffersQuery, BaseResponse<PagedResponse<ApplicationOfferResponse>>>
    {
        private readonly IGetApplicationOffers _service;
        public GetApplicationOffersHandler(IGetApplicationOffers service)
        {
            _service = service;
        }

        public async Task<BaseResponse<PagedResponse<ApplicationOfferResponse>>> Handle(GetApplicationOffersQuery request, CancellationToken ct)
        {
            return await _service.GetAllAsync(request.request, ct);
        }
    }
}
