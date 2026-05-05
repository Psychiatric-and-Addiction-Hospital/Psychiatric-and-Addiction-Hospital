using Application.Commands.HR.ApplicationOffer;
using Application.Commands.HR.ApplicationProcess;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers.HR
{
    public class ApplicationOfferController : BaseController
    {
        private readonly ISender _sender;
        public ApplicationOfferController(ISender sender)
        {
            _sender = sender;
        }
        [HttpPost("CreateApplicationOffer")]
        public async Task<IActionResult> CreateApplicationOffer([FromBody] CreateApplicationOfferCommand request)
        {
            var result= await _sender.Send(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("UpdateApplicationOfferStatus/{Id}")]
        public async Task<IActionResult> UpdateApplicationOfferStatus([FromRoute] Guid Id, [FromBody]
        OfferStatues Statues)
        {
            var result = await _sender.Send(new UpdateApplicationOfferStatusCommand(Id, Statues));

            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
