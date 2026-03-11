using Application.Commands.Patient;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers.Patient
{

    public class PublicBookingController : BaseController
    {
        private readonly ISender _Sender;
        public PublicBookingController(ISender sender)
        {
            _Sender = sender;
        }

        [HttpPost("CreatePublicBooking")]
        public async Task<IActionResult> Create([FromBody] CreatePublicBookingCommand request)
        {
            var result = await _Sender.Send(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}