using Application.Commands.HR.ApplicationOffer;
using Application.Common.Interfaces.HR.ApplicationOffer;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationOffer;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.ApplicationOffer
{
    public class CreateApplicationOfferHandler : IRequestHandler<CreateApplicationOfferCommand, BaseResponse<ApplicationOfferResponse>>
    {
        private readonly ICreateApplicationOffer _service;
        public CreateApplicationOfferHandler(ICreateApplicationOffer service)
        {
            _service = service;
        }
        public async Task<BaseResponse<ApplicationOfferResponse>> Handle(CreateApplicationOfferCommand request, CancellationToken ct)
        {
            return await _service.CreateAsync(request.request, ct);
        }
    }
}
