using Application.Commands.Authentication;
using Domain.Entites;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Authentication
{
    internal class ResetPasswordHandler:IRequestHandler<ResetPasswordCommand, Result<string>>
    {
        private readonly UserManager<AppUser> _userManager;

        public ResetPasswordHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                return Result.Fail("User not found.");

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, resetToken, request.NewPassword);

            if (!result.Succeeded)
                return Result.Fail("Password reset failed.");

            return Result.Ok("Password reset successfully.");
        }
    }
}
