using Application.Commands.Doctores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers
{
    public class DoctorApplicationController : BaseController
    {
        private readonly ISender _sender;
        public DoctorApplicationController(ISender sender)
        {
            _sender = sender;
        }
        [HttpPost("Apply")]
        public async Task<IActionResult> Apply([FromBody] DoctorApplyCommand command)
        {
            var result = await _sender.Send(command);
            if (result.IsFailed)
                return BadRequest(result.Errors);
            return Ok(result.Value);
        }
    }
}
