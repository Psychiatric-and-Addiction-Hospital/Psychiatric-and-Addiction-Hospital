using Application.Commands.HR.Candidate;
using Application.Common.Responses;
using Application.DTOS.Request.HR.Candidate;
using Application.Queries.HR.Candidate;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers.HR
{

    public class CandidateController : BaseController
    {
        private readonly ISender _sender;
        public CandidateController(ISender sender)
        {
            _sender = sender;
        }

        [AllowAnonymous]
        [HttpPost("CreateCandidate")]
        public async Task<IActionResult> Create([FromForm] CreateCandidateRequest request)
        {
            var result = await _sender.Send(new CreateCandidateCommand(request));
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("account")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAccount([FromBody] CreateCandidateAccountRequest request)
        {
            var result = await _sender.Send(new CreateCandidateAccountCommand(request));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Policy = "HRManagement")]
        [HttpPut("{id:guid}/UpdateCandidate")]
        public async Task<IActionResult> Update([FromRoute] Guid Id, [FromForm] UpdateCandidateRequest request)
        {
            if (Id != request.Id)
                return BadRequest(ResponseFactory.Fail<bool>("Route id does not match request id."));

            var result = await _sender.Send(new UpdateCandidateCommand(request));
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Policy = "HRManagement")]
        [HttpDelete("{id:guid}/DeleteCandidate")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _sender.Send(new DeleteCandidateCommand(id));
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Policy = "HRManagement")]
        [HttpGet("{id:guid}/GetCandidateById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _sender.Send(new GetCandidateByIdQuery(id));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Policy = "HRManagement")]
        [HttpGet("GetAllCandidate")]
        public async Task<IActionResult> GetAll([FromQuery] CandidateListRequest request)
        {
            var result = await _sender.Send(new GetCandidatesQuery(request));

            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
