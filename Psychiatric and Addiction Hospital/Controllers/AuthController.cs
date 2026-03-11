using Application.Commands.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers
{
    public class AuthController : BaseController
    {
        private readonly ISender _sender;
        public AuthController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterCommand command)
        {
            var result = await _sender.Send(command);

            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _sender.Send(command);

            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(SendForgetPasswordOtpCommand command)
        {
            var result = await _sender.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp(VerifyOtpCommand command)
        {
            var result = await _sender.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
        {
            var result = await _sender.Send(command);
            return result.Success ? Ok(result) : BadRequest(result.Errors);
        }


        [HttpPost("refreshToken")]
        public async Task<IActionResult> Refresh(RefreshTokenCommand command)
        {
            var result = await _sender.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
