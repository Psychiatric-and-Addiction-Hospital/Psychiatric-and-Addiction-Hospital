using Application.Commands.Authentication;
using Application.Common.Responses;
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
using static System.Net.Mime.MediaTypeNames;

namespace Application.Handlers.Authentication
{
    internal class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, BaseResponse<string>>
    {
        private readonly UserManager<AppUser> _userManager;

        public ResetPasswordHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<BaseResponse<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                return ResponseFactory.Fail<string>("User not found.");

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, resetToken, request.NewPassword);

            if (!result.Succeeded)
                return ResponseFactory.Fail<string>("Password reset failed.");

            return ResponseFactory.Success("Password reset successfully.");
            
        }
    }
}
