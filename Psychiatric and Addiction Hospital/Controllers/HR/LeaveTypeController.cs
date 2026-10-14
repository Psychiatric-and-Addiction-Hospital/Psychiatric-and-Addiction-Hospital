using Application.Commands.HR.LeaveType;
using Application.Common.Responses;
using Application.DTOS.Request.HR.LeaveType;
using Application.Queries.HR.LeaveType;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers.HR
{
    public class LeaveTypeController : BaseController
    {
        private readonly ISender _sender;

        public LeaveTypeController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize(Policy = "HRManagement")]
        [HttpPost("CreateLeaveType")]
        public async Task<IActionResult> Create(CreateLeaveTypeRequest request)
        {
            var result = await _sender.Send(new CreateLeaveTypeCommand(request));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Policy = "HRManagement")]
        [HttpPut("{Id:guid}UpdateLeaveType")]
        public async Task<IActionResult> Update([FromRoute] Guid Id,[FromBody] UpdateLeaveTypeRequest request)
        {
            if (Id == request.LeaveTypeId)
                return BadRequest(ResponseFactory.Fail<bool>("Route id does not match request id."));

            var result = await _sender.Send(new UpdateLeaveTypeCommand(request));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{Id:guid}DeleteLeaveType")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id, DeleteLeaveTypeRequest request)
        {
            if (Id == request.LeaveTypeId)
                return BadRequest(ResponseFactory.Fail<bool>("Route id does not match request id."));

            var result = await _sender.Send(new DeleteLeaveTypeCommand(request));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Policy = "HospitalStaff")]
        [HttpGet("GetLeaveTypes")]
        public async Task<IActionResult> GetAll(LeaveTypeListRequest request)
        {
            var result = await _sender.Send(new GetLeaveTypesQuery(request));

            return result.Success ? Ok(result) : BadRequest(result);
        }
        [Authorize(Policy = "HospitalStaff")]
        [HttpGet("{Id:guid}GetLeaveTypeById")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            var result = await _sender.Send(new GetleaveTypeByIdQuery(Id));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{Id:guid}RestoreLeaveType")]
        public async Task<IActionResult> Restore([FromRoute] Guid Id)
        {
            var result = await _sender.Send(new RestoreLeaveTypeCommand(Id));

            return result.Success ? Ok(result) : BadRequest(result);
        }

    }
}
