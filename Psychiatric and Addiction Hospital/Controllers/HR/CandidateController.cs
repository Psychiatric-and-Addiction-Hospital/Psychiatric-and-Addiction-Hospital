using Application.Commands.HR.Candidate;
using Application.Queries.HR.Candidate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers.HR
{
    
    public class CandidateController : BaseController
    {
        private readonly ISender _Sender;
        public CandidateController(ISender sender)
        {
            _Sender = sender;
        }
        [HttpPost("CreateCandidate")]
        public async Task<IActionResult> CreateCandidate([FromForm]CreateCandidateCommand request)
        {
            var result = await _Sender.Send(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("UpdateCandidate")]
        public async Task<IActionResult> UpdateCandidate([FromForm] UpdateCandidateCommand request)
        {
            var result = await _Sender.Send(request);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("DeleteCandidate/{id}")]
        public async Task<IActionResult> DeleteCandidate(Guid id)
        {
            var result = await _Sender.Send(new DeleteCandidateCommand(id));

            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpGet("GetCandidateById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _Sender.Send(new GetCandidateByIdQuery(id));
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetAllCandidates")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _Sender.Send(new GetAllCandidatesQuery());
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
