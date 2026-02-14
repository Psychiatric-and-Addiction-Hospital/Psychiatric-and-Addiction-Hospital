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
        [HttpPost("approve/{id}")]
        public async Task<IActionResult> ApproveDoctor(Guid id)
        {
            var result = await _sender.Send(new ApproveDoctorCommand(id));

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("reject/{id}")]
        public async Task<IActionResult> RejectDoctor(Guid id,[FromBody]string Reason)
        {
            var result = await _sender.Send(new RejectDoctorCommand(id,Reason));

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
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
