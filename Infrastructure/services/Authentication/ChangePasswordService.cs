using Application.Common.Interfaces.Authentication;
using Application.Common.Responses;
using Application.DTOS.Request.Authentication;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;


namespace Infrastructure.services.Authentication
{
    public class ChangePasswordService : IChangePassword
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICurrentUser _currentUser;
        public ChangePasswordService(UserManager<AppUser> userManager, ICurrentUser currentUser)
        {
            _userManager = userManager;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<bool>> ChangeAsync(ChangePasswordRequest request, CancellationToken ct)
        {
            if (!_currentUser.IsAuthenticated)
                return ResponseFactory.Fail<bool>("User is not authenticated.");

            if (request.NewPassword != request.ConfirmPassword)
                return ResponseFactory.Fail<bool>("New password and confirmation password do not match.");

            var user = await _userManager.FindByIdAsync(request.CurrentPassword);


            if (user == null)
                return ResponseFactory.Fail<bool>("User not found.");

            var result = await _userManager.ChangePasswordAsync(user, request.NewPassword, request.ConfirmPassword);

            if (!result.Succeeded)
                return ResponseFactory.Fail<bool>("Failed to change password."
                    , result.Errors.Select(x => x.Description).ToList());

            return ResponseFactory.Success(true, "Password changed successfully.");

        }
    }
}
