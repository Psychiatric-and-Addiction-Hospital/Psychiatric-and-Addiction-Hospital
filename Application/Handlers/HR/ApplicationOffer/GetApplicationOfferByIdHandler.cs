using Application.Common.Interfaces.HR.ApplicationOffer;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationOffer;
using Application.Queries.HR.ApplicationOffer;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.ApplicationOffer
{
    public class GetApplicationOfferByIdHandler : IRequestHandler<GetApplicationOfferByIdQuery, BaseResponse<ApplicationOfferResponse>>
    {
        private readonly IGetApplicationOfferById _service;
        public GetApplicationOfferByIdHandler(IGetApplicationOfferById service)
        {
            _service = service;
        }
        public async Task<BaseResponse<ApplicationOfferResponse>> Handle(GetApplicationOfferByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
