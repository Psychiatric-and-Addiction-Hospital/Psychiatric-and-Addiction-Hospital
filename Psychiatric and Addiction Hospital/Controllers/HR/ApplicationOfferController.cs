using Application.Commands.HR.ApplicationOffer;
using Application.Common.Responses;
using Application.DTOS.Request.HR.ApplicationOffer;
using Application.Queries.HR.ApplicationOffer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers.HR
{
    [Authorize(Policy = "HRManagement")]
    public class ApplicationOfferController : BaseController
    {
        private readonly ISender _sender;
        public ApplicationOfferController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("GetAllApplicationOffers")]
        public async Task<IActionResult> GetAll([FromQuery] ApplicationOfferListRequest request)
        {
            var result = await _sender.Send(new GetApplicationOffersQuery(request));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id:guid}/GetByIdApplicationOffer")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _sender.Send(new GetApplicationOfferByIdQuery(id));

            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost("CreateApplicationOffer")]
        public async Task<IActionResult> Create([FromBody] CreateApplicationOfferRequest request)
        {
            var result = await _sender.Send(new CreateApplicationOfferCommand(request));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{id:guid}/UpdateApplicationOffer")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateApplicationOfferRequest request)
        {
            if (id != request.Id)
                return BadRequest(ResponseFactory.Fail<bool>("Route id does not match request id."));

            var result = await _sender.Send(new UpdateApplicationOfferCommand(request));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id:guid}/DeleteApplicationOffer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _sender.Send(new DeleteApplicationOfferCommand(id));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        

    }

}