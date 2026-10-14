using Application.Commands.HR.Shift;
using Application.Common.Responses;
using Application.DTOS.Request.HR.Shift;
using Application.Queries.HR.Shift;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers.HR
{
    public class ShiftController : BaseController
    {
        private readonly ISender _sender;
        public ShiftController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize(Policy = "HospitalStaff")]
        [HttpGet("{id:guid}/GetShiftById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _sender.Send(new GetShiftByIdQuery(id));
            return result.Success ? Ok(result) : NotFound(result);
        }

        [Authorize(Policy = "HRManagement")]
        [HttpPost("CreateShift")]
        public async Task<IActionResult> Create([FromBody] CreateShiftRequest request)
        {
            var result = await _sender.Send(new CreateShiftCommand(request));
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Policy = "HospitalStaff")]
        [HttpGet("GetAllShift")]
        public async Task<IActionResult> GetAll([FromQuery] ShiftListRequest request)
        {
            var result = await _sender.Send(new GetAllShiftsQuery(request));
            return result.Success ? Ok(result) : NotFound(result);
        }

        [Authorize(Policy = "HRManagement")]
        [HttpPut("{id:guid}/UpdateShift")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateShiftRequest request)
        {
            if (id != request.Id)
                return BadRequest(ResponseFactory.Fail<bool>("Route id does not match request id."));
            var result = await _sender.Send(new UpdateShiftCommand(request));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id:guid}/DeleteShift")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _sender.Send(new DeleteShiftCommand(id));

            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
