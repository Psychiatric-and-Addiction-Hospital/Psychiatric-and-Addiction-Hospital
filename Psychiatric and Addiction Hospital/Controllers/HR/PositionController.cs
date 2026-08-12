using Application.Commands.HR.Position;
using Application.Commands.HR.Shift;
using Application.Common.Responses;
using Application.DTOS.Request.HR.Position;
using Application.DTOS.Request.HR.Shift;
using Application.Queries.HR.Position;
using Application.Queries.HR.Shift;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers.HR
{

    public class PositionController : BaseController
    {
        private readonly ISender _sender;

        public PositionController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize(Policy = "HRManagement")]
        [HttpPost("CreatePosition")]
        public async Task<IActionResult> Create([FromBody] CreatePositionRequest request)
        {
            var result = await _sender.Send(new CreatePositionCommand(request));
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Policy = "HRManagement")]
        [HttpPut("{id:guid}/UpdatePosition")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatePositionRequest request)
        {
            if (id != request.Id)
                return BadRequest(ResponseFactory.Fail<bool>("Route id does not match request id."));
            var result = await _sender.Send(new UpdatePositionCommand(request));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id:guid}/DeletePosition")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _sender.Send(new DeletePositionCommand(id));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [AllowAnonymous]
        [HttpGet("GetAllPosition")]
        public async Task<IActionResult> GetAll([FromQuery] PositionListRequest request)
        {
            var result = await _sender.Send(new GetPositionsQuery(request));
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}/GetPositionById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _sender.Send(new GetPositionByIdQuery(id));
            return result.Success ? Ok(result) : NotFound(result);
        }
    }
}
