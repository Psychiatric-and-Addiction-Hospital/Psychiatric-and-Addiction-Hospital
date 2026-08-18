using Application.Commands.HR.JobPosting;
using Application.Common.Responses;
using Application.DTOS.Request.HR.JobPosting;
using Application.Queries.HR.JobPosting;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers.HR
{
    public class JobPostingController : BaseController
    {
        private readonly ISender _sender;
        public JobPostingController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize(Policy = "HRManagement")]
        [HttpPut("{id:guid}/CloseJobPosting")]
        public async Task<IActionResult> Close(Guid id)
        {
            var result = await _sender.Send(new CloseJobPostingCommand(id));
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Policy = "HRManagement")]
        [HttpPut("{id:guid}/PublishJobPosting")]
        public async Task<IActionResult> Publish(Guid id)
        {
            var result = await _sender.Send(new PublishJobPostingCommand(id));
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}/GetJobPostingById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _sender.Send(new GetJobPostingByIdQuery(id));

            return result.Success ? Ok(result) : NotFound(result);
        }

        [AllowAnonymous]
        [HttpGet("GetJobPostings")]
        public async Task<IActionResult> GetAll([FromQuery] JobPostingListRequest request)
        {
            var result = await _sender.Send(new GetJobPostingsQuery(request));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Policy = "HRManagement")]
        [HttpPost("CreateJobPosting")]
        public async Task<IActionResult> Create([FromForm] CreateJobPostingRequest request)
        {
            var result = await _sender.Send(new CreateJobPostingCommand(request));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Policy = "HRManagement")]
        [HttpPut("{id:guid}/UpdateJobPosting")]
        public async Task<IActionResult> Update(Guid id, [FromForm] UpdateJobPostingRequest request)
        {
            if (id != request.Id)
                return BadRequest(ResponseFactory.Fail<bool>("Route id does not match request id."));

            var result = await _sender.Send(new UpdateJobPostingCommand(request));

            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
