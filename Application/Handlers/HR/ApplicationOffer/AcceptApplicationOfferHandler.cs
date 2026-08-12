using Application.Commands.HR.ApplicationOffer;
using Application.Common.Interfaces.HR.ApplicationOffer;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationOffer;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.ApplicationOffer
{
    public class AcceptApplicationOfferHandler : IRequestHandler<AcceptApplicationOfferCommand, BaseResponse<ApplicationOfferResponse>>
    {
        private readonly IAcceptApplicationOffer _service;

        public AcceptApplicationOfferHandler(IAcceptApplicationOffer service)
        {
            _service = service;
        }

        public async Task<BaseResponse<ApplicationOfferResponse>> Handle(AcceptApplicationOfferCommand request, CancellationToken ct)
        {
            return await _service.AcceptAsync(request.OfferId, ct);
        }
    }
}
