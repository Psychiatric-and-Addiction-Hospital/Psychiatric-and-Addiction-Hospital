using Application.Commands.HR.Application;
using Application.Common.Responses;
using Application.DTOS.Request.HR.Application;
using Application.Queries.HR.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers.HR
{
    public class ApplicationController : BaseController
    {
        private readonly ISender _sender;
        public ApplicationController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize(Policy = "HRManagement")]
        [HttpGet("GetAllApplication")]
        public async Task<IActionResult> GetAll([FromQuery] ApplicationListRequest request)
        {
            var result = await _sender.Send(new GetApplicationsQuery(request));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Policy = "HRManagement")]
        [HttpGet("{id:guid}/GetApplicationById")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _sender.Send(new GetApplicationByIdQuery(id));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [AllowAnonymous]
        [HttpPost("CreateApplication")]
        public async Task<IActionResult> Create([FromBody] CreateApplicationRequest request)
        {
            var result = await _sender.Send(new CreateApplicationCommand(request));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Policy = "HROnly")]
        [HttpPut("{id:guid}/UpdateApplicationStatus")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateApplicationStatusRequest request)
        {
            if (id != request.Id)
                return BadRequest(ResponseFactory.Fail<bool>("Route id does not match request id."));

            var result = await _sender.Send(new UpdateApplicationStatusCommand(request));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id:guid}/DeleteApplication")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _sender.Send(new DeleteApplicationCommand(id));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        //[HttpPut("{id:guid}/WithdrawApplication")]
        //public async Task<IActionResult> Withdraw(Guid id)
        //{
        //    var result = await _sender.Send(new WithdrawApplicationCommand(id));

        //    return result.Success ? Ok(result) : BadRequest(result);
        //}
    }
}
