using Application.Commands.Patient;
using Application.DTOS.Request.Patient;
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
        public async Task<IActionResult> Create([FromBody] CreatePublicBookingRequest request)
        {
            var result = await _Sender.Send(new CreatePublicBookingCommand(request));
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}