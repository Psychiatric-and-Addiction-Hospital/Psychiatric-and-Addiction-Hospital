using Application.Commands.EmployeeProfile;
using Application.DTOS.Request.EmployeeProfile;
using Application.Queries.EmployeeProfile;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers
{

    public class EmployeeProfileController : BaseController
    {
        private readonly ISender _sender;
        public EmployeeProfileController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("GetMyProfileEmployee")]
        public async Task<IActionResult> GetMyProfileEmployee()
        {
            var result = await _sender.Send(new GetMyProfileQuery());
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPut("UpdateMyProfile")]
        public async Task<IActionResult> UpdateMyProfileEmployee([FromBody] UpdateMyProfileRequest request)
        {
            var result = await _sender.Send(new UpdateMyProfileCommand(request));
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
