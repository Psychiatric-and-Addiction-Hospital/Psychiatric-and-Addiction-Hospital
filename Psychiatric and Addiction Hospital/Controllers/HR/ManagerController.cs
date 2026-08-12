using Application.Commands.HR.Manager;
using Application.Common.Responses;
using Application.DTOS.Request.HR.manager;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers.HR
{
    [Authorize(Policy = "HRManagement")]
    public class ManagerController : BaseController
    {
        private readonly ISender _sender;
        public ManagerController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPut("{departmentId:guid}/AssignManager")]
        public async Task<IActionResult> AssignManager(Guid departmentId, [FromBody] AssignDepartmentManagerRequest request)
        {
            if (departmentId != request.DepartmentId)
                return BadRequest(ResponseFactory.Fail<bool>("Route id does not match request id."));

            var result = await _sender.Send(new AssignDepartmentManagerCommand(request));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{departmentId:guid}/ChangeManager")]
        public async Task<IActionResult> ChangeManager(Guid departmentId, [FromBody] ChangeDepartmentManagerRequest request)
        {
            if (departmentId != request.DepartmentId)
                return BadRequest(ResponseFactory.Fail<bool>("Route id does not match request id."));

            var result = await _sender.Send(new ChangeDepartmentManagerCommand(request));

            return result.Success ? Ok(result) : BadRequest(result);
        }


        [HttpPut("{departmentId:guid}/RemoveManager")]
        public async Task<IActionResult> RemoveManager(Guid departmentId)
        {
            var result = await _sender.Send(new RemoveDepartmentManagerCommand(departmentId));

            return result.Success ? Ok(result) : BadRequest(result);
        }

    }
}
