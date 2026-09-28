using Application.Commands.Patient;
using Application.Queries.Patient;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Psychiatric_and_Addiction_Hospital.Controllers.Patient
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientProfileController : BaseController
    {
        private readonly ISender _sender;

        public PatientProfileController(ISender sender)
        {
            _sender = sender;
        }


        [HttpGet("GetProfile/{userId}")]
        public async Task<IActionResult> GetProfile(string userId, CancellationToken ct)
        {
            var result = await _sender.Send(new GetPatientProfileQuery(userId), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

       
        [HttpPut("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdatePatientProfileCommand request, CancellationToken ct)
        {
            var result = await _sender.Send(request, ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

     
        [HttpPut("UploadImage")]
        public async Task<IActionResult> UploadImage([FromBody] UploadPatientImageCommand request, CancellationToken ct)
        {
            var result = await _sender.Send(request, ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
