using Application.Commands.HR.ApplicationOffer;
using Application.Common.Interfaces.HR.ApplicationOffer;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationOffer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.ApplicationOffer
{
    internal class UpdateApplicationOfferHandler : IRequestHandler<UpdateApplicationOfferCommand, BaseResponse<ApplicationOfferResponse>>
    {
        private readonly IUpdateApplicationOffer _service;
        public UpdateApplicationOfferHandler(IUpdateApplicationOffer service)
        {
            _service = service;
        }
        public async Task<BaseResponse<ApplicationOfferResponse>> Handle(UpdateApplicationOfferCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateAsync(request.request, cancellationToken);
        }
    }
}
