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

        /// <summary>
        /// Get patient profile by UserId
        /// </summary>
        [HttpGet("GetProfile/{userId}")]
        public async Task<IActionResult> GetProfile(string userId, CancellationToken ct)
        {
            var result = await _sender.Send(new GetPatientProfileQuery(userId), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        /// <summary>
        /// Update patient info (name, DOB, gender, marital status, occupation, address, phone)
        /// </summary>
        [HttpPut("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdatePatientProfileCommand request, CancellationToken ct)
        {
            var result = await _sender.Send(request, ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Upload / update patient profile image (send ImageUrl as string)
        /// </summary>
        [HttpPut("UploadImage")]
        public async Task<IActionResult> UploadImage([FromBody] UploadPatientImageCommand request, CancellationToken ct)
        {
            var result = await _sender.Send(request, ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
