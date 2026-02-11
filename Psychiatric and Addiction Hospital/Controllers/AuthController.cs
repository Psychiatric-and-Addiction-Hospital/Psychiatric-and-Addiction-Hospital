using Application.Commands.Authentication;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            var result = await _sender.Send(command);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await _sender.Send(command);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(SendForgetPasswordOtpCommand command)
        {
            var result = await _sender.Send(command);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp(VerifyOtpCommand command)
        {
            var result = await _sender.Send(command);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
        {
            var result = await _sender.Send(command);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
        }


        [HttpPost("refreshToken")]
        public async Task<IActionResult> Refresh(RefreshTokenCommand command)
        {
            var result = await _sender.Send(command);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }
    }
}
