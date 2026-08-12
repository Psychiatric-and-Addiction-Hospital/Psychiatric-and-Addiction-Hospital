using Application.Commands.HR.ApplicationOffer;
using Application.Common.Interfaces.HR.ApplicationOffer;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationOffer;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.ApplicationOffer
{
    public class RejectApplicationOfferHandler : IRequestHandler<RejectApplicationOfferCommand, BaseResponse<ApplicationOfferResponse>>
    {
        private readonly IRejectApplicationOffer _service;
        public RejectApplicationOfferHandler(IRejectApplicationOffer service)
        {
            _service = service;
        }

        public async Task<BaseResponse<ApplicationOfferResponse>> Handle(RejectApplicationOfferCommand request, CancellationToken ct)
        {
            return await _service.RejectAsync(request.OfferId, ct);
        }
    }
}
