using Application.Commands.HR.Recruitment;
using Application.Queries.HR.Recruitment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers.HR
{

    public class RecruitmentController : BaseController
    {
        private readonly ISender _sender;
        public RecruitmentController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("CreateRecruitment")]
        public async Task<IActionResult> CreateRecruitment([FromBody] CreateRecruitmentCommand request)
        {
            var result = await _sender.Send(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("UpdateRecruitment")]

        public async Task<IActionResult> UpdateRecruitment(UpdateRecruitmentCommand request)
        {
            var result = await _sender.Send(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("DeleteRecruitment/{RecruitmentId}")]
        public async Task<IActionResult> DeleteRecruitment(Guid RecruitmentId)
        {
            var result = await _sender.Send(new DeleteRecruitmentCommand(RecruitmentId));
            return result.Success ? Ok(result) : BadRequest();
        }

        [HttpGet("GetAllRecruitment")]
        public async Task<IActionResult> GetAllRecruitment()
        {
            var result = await _sender.Send(new GetRecruitmentQuery());
            return result.Success ? Ok(result) : BadRequest();
        }
    }
}