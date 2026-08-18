using Application.Commands.HR.ApplicationInterview;
using Application.Common.Responses;
using Application.DTOS.Request.HR.ApplicationInterview;
using Application.Queries.HR.ApplicationInterview;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers.HR
{
    [Authorize(Policy = "HRManagement")]
    public class ApplicationInterviewController : BaseController
    {
        private readonly ISender _sender;
        public ApplicationInterviewController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{id:guid}/GetApplicationInterviewById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _sender.Send(new GetApplicationInterviewByIdQuery(id));

            return result.Success ? Ok(result) : NotFound(result);
        }
        [HttpPost("CreateApplicationInterview")]
        public async Task<IActionResult> Create([FromForm] CreateApplicationInterviewRequest request)
        {
            var result = await _sender.Send(new CreateApplicationInterviewCommand(request));
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetAllApplicationInterview")]
        public async Task<IActionResult> GetAll([FromQuery] ApplicationInterviewListRequest request)
        {
            var result = await _sender.Send(new GetApplicationInterviewsQuery(request));
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPut("{id:guid}/UpdateApplicationInterview")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateApplicationInterviewRequest request)
        {
            if (id != request.Id)
                return BadRequest(ResponseFactory.Fail<bool>("Route id does not match request id."));

            var result = await _sender.Send(new UpdateApplicationInterviewCommand(request));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{id:guid}/CompleteInterview")]
        public async Task<IActionResult> Complete(Guid id, [FromBody] CompleteInterviewRequest request)
        {
            if (id != request.Id)
                return BadRequest(ResponseFactory.Fail<bool>("Route id does not match request id."));

            var result = await _sender.Send(new CompleteInterviewCommand(request));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{id:guid}/CancelInterview")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var result = await _sender.Send(new CancelInterviewCommand(id));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id:guid}/DeleteApplicationInterview")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _sender.Send(new DeleteApplicationInterviewCommand(id));

            return result.Success ? Ok(result) : NotFound(result);
        }
    }
}