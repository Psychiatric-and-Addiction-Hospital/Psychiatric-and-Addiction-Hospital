using Application.Commands.HR.ApplicationOffer;
using Application.Common.Interfaces.HR.ApplicationOffer;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.ApplicationOffer
{
    public class UpdateApplicationOfferStatusHandler:IRequestHandler<UpdateApplicationOfferStatusCommand,BaseResponse<ApplicationOfferResponse>>
    {
        private readonly IUpdateApplicationOfferStatus _updateofferStatus;
        public UpdateApplicationOfferStatusHandler(IUpdateApplicationOfferStatus updateofferStatus)
        {
            _updateofferStatus = updateofferStatus;
        }

        public async Task<BaseResponse<ApplicationOfferResponse>> Handle(UpdateApplicationOfferStatusCommand request, CancellationToken ct)
        {
            return await _updateofferStatus.UpdateApplicationOfferStatusAsync(request.OfferId, request.Status, ct);
        }
    }
}
