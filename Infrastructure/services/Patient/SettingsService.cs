using Application.Commands.Patient;
using Application.Common.Interfaces.Patient;
using Application.Common.Responses;
using Infrastructure.Persistence.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.services.Patient
{
    public class SettingsService : ISettingsService
    {
        private readonly UserManager<Domain.Entites.AppUser> _userManager;
        private readonly AddIdentityDbContext _context;

        public SettingsService(UserManager<Domain.Entites.AppUser> userManager, AddIdentityDbContext context)
        {
            _userManager = userManager;
            _context     = context;
        }

        // ── CHANGE PASSWORD ──────────────────────────────────────────────────────
        public async Task<BaseResponse<Unit>> ChangePasswordAsync(ChangePasswordCommand command, CancellationToken ct)
        {
            if (command.NewPassword != command.ConfirmNewPassword)
                return ResponseFactory.Fail<Unit>("New password and confirmation do not match");

            var user = await _userManager.FindByIdAsync(command.UserId);
            if (user == null)
                return ResponseFactory.Fail<Unit>("User not found");

            var result = await _userManager.ChangePasswordAsync(user, command.CurrentPassword, command.NewPassword);
            if (!result.Succeeded)
                return ResponseFactory.Fail<Unit>("Password change failed",
                    new List<string>(System.Linq.Enumerable.Select(result.Errors, e => e.Description)));

            return ResponseFactory.Success(Unit.Value, "Password changed successfully");
        }

        // ── GET PREFERENCES ──────────────────────────────────────────────────────
        // NOTE: Preferences are stored on AppUser if you add those fields,
        // For now we return sensible defaults — extend AppUser entity to persist them.
        public async Task<BaseResponse<PreferencesResponse>> GetPreferencesAsync(string userId, CancellationToken ct)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId, ct);
            if (user == null)
                return ResponseFactory.Fail<PreferencesResponse>("User not found");

            // Default preferences — extend AppUser with these fields to persist them
            return ResponseFactory.Success(new PreferencesResponse
            {
                EmailNotifications = true,
                SmsNotifications   = true,
                AppNotifications   = true,
                Language           = "ar"
            }, "Preferences retrieved");
        }

        // ── UPDATE PREFERENCES ───────────────────────────────────────────────────
        public async Task<BaseResponse<Unit>> UpdatePreferencesAsync(UpdatePreferencesCommand command, CancellationToken ct)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == command.UserId, ct);
            if (user == null)
                return ResponseFactory.Fail<Unit>("User not found");

            // Extend AppUser with preference columns to persist here
            // e.g. user.EmailNotifications = command.EmailNotifications;
            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(Unit.Value, "Preferences updated successfully");
        }
    }
}
