using Application.Commands.Admin;
using Application.Queries.Admin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers
{

    public class AdminController : BaseController
    {
        private readonly ISender _sender;
        public AdminController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("approve/{DoctorId}/dept{DepartmentId}")]
        public async Task<IActionResult> ApproveDoctor(Guid DoctorId, Guid DepartmentId)
        {
            var result = await _sender.Send(new ApproveDoctorCommand(DoctorId, DepartmentId));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("reject/{DoctorId}")]
        public async Task<IActionResult> RejectDoctor(Guid DoctorId,[FromBody]string Reason)
        {
            var result = await _sender.Send(new RejectDoctorCommand(DoctorId,Reason));

            return result.Success ? Ok(result) : BadRequest(result);
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("pending")]
        public async Task<IActionResult> GetPending()
        {
            var result = await _sender.Send(new GetPendingDoctorsQuery());
            return Ok(result);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("approved")]
        public async Task<IActionResult> GetApproved()
        {
            var result = await _sender.Send(new GetApprovedDoctorsQuery());
            return Ok(result);
        }
    }
}
