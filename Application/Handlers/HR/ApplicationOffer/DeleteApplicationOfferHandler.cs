using Application.Commands.HR.ApplicationOffer;
using Application.Common.Interfaces.HR.ApplicationOffer;
using Application.Common.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.ApplicationOffer
{
    public class DeleteApplicationOfferHandler : IRequestHandler<DeleteApplicationOfferCommand, BaseResponse<bool>>
    {
        private readonly IDeleteApplicationOffer _service;
        public DeleteApplicationOfferHandler(IDeleteApplicationOffer service)
        {
            _service = service;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteApplicationOfferCommand request, CancellationToken ct)
        {
            return await _service.DeleteAsync(request.OfferId, ct);
        }
    }
}
