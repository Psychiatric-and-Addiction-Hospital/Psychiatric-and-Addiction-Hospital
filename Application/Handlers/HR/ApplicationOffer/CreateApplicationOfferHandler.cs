using Application.Commands.HR.ApplicationOffer;
using Application.Common.Interfaces.HR.ApplicationOffer;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.ApplicationOffer
{
    public class CreateApplicationOfferHandler: IRequestHandler<CreateApplicationOfferCommand,BaseResponse<ApplicationOfferResponse>>
    {
        private readonly ICreateApplicationOffer _createApplicationOffer;
        public CreateApplicationOfferHandler(ICreateApplicationOffer createApplicationOffer)
        {
            _createApplicationOffer = createApplicationOffer;
        }

        public async Task<BaseResponse<ApplicationOfferResponse>> Handle(CreateApplicationOfferCommand request, CancellationToken ct)
        {
            return await _createApplicationOffer.CreateApplicationOfferAsync(request.ApplicationProcessId,request.OfferedSalary,request.ExpiryDate,ct);
        }
    }
}
