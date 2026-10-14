using Application.Commands.HR.Department;
using Application.Common.Responses;
using Application.Queries.Depertment;
using Application.Queries.Depertments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers.HR
{

    public class DepartmentController : BaseController
    {
        private readonly ISender _sender;
        public DepartmentController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize(Policy = "HRManagement")]
        [HttpPost("CreateDepartment")]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentCommand request)
        {
            var result = await _sender.Send(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Policy = "HRManagement")]
        [HttpPut("{Id:guid}/UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment([FromRoute] Guid Id, [FromBody] UpdateDepartmentCommand request)
        {
            if (Id != request.Id)
                return BadRequest(ResponseFactory.Fail<bool>("Route id does not match request id."));

            var result = await _sender.Send(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{Id:guid}/DeleteDepartment")]
        public async Task<IActionResult> DeleteDepartment( Guid Id)
        {
            var result = await _sender.Send(new DeleteDepartmentCommand(Id));
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [AllowAnonymous]
        [HttpGet("GetAllDepertment")]
        public async Task<IActionResult> GetAllDepertment()
        {
            var result = await _sender.Send(new GetDepertmentQuery());
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [AllowAnonymous]
        [HttpGet("{Id:guid}/GetDepertmentById")]
        public async Task<IActionResult> GetDepertmentById(Guid Id)
        {
            var result = await _sender.Send(new GetDepertmentByIdQuery(Id));
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
