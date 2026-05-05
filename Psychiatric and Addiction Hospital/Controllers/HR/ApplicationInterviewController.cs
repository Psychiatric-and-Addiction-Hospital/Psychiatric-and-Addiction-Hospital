using Application.Commands.HR.ApplicationInterview;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers.HR
{

    public class ApplicationInterviewController : BaseController
    {
        private readonly ISender _sender;
        public ApplicationInterviewController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("CreateApplicationInterview")]
        public async Task<IActionResult> CreateApplicationInterview([FromBody] CreateApplicationInterviewCommand command)
        {
            var result = await _sender.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("UpdateInterviewResult")]
        public async Task<IActionResult> UpdateInterviewResult([FromBody] UpdateInterviewResultCommand command)
        {
            var result = await _sender.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}