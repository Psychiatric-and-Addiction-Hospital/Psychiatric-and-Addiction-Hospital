using Application.Commands.HR.ApplicationProcess;
using Application.Queries.HR.ApplicationProcesses;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers.HR
{
    public class ApplicationProcessController : BaseController
    {
        private readonly ISender _sender;
        public ApplicationProcessController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("CreateApplicationProcess")]
        public async Task<IActionResult> CreateApplicationProcess([FromBody] CreateApplicationProcessCommand request)
        {
            var result= await _sender.Send(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("UpdateApplicationProcessStatus/{id}")]
        public async Task<IActionResult> UpdateApplicationProcessStatus( [FromRoute] Guid id,[FromBody]
        ApplicationStatus status)
        {
            var result = await _sender.Send(new UpdateApplicationProcessStatusCommand(id, status));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetAllApplicationProcesses")]
        public async Task<IActionResult> GetAllApplicationProcesses()
        {
            var result = await _sender.Send(new GetAllApplicationProcessesQuery());
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
