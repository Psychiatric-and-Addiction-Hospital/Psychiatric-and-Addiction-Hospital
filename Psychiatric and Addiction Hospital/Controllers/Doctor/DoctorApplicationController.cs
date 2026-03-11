using Application.Commands.Doctores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers.Doctor
{
    public class DoctorApplicationController : BaseController
    {
        private readonly ISender _sender;
        public DoctorApplicationController(ISender sender)
        {
            _sender = sender;
        }
        [HttpPost("Apply")]
        public async Task<IActionResult> Apply([FromForm] DoctorApplyCommand request)
        {
            var result = await _sender.Send(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
