using Application.Commands.Profile;
using Application.DTOS.Request.Profile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers
{

    public class ProfileController : BaseController
    {
        private readonly ISender _sender;
        public ProfileController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPut("ChangeProfileImage")]
        public async Task<IActionResult> ChangeProfileImage([FromForm] ChangeProfileImageRequest request)
        {
            var result = await _sender.Send(new ChangeProfileImageCommand(request));

            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
