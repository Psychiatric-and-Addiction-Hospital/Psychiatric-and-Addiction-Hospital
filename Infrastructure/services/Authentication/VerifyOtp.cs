using Application.Common.Interfaces.Authentication;
using Domain.Entites;
using FluentResults;
using Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.Authentication
{
    public class VerifyOtp:IVerifyOtp
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AddIdentityDbContext _context;
        public VerifyOtp(UserManager<AppUser> userManager, AddIdentityDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<Result<string>> VerifyOtpAsync(string email, string code, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return Result.Fail("Invalid Email.");

            var resetCode = await _context.PasswordResetCodes
                .Where(x => x.UserId == user.Id && !x.IsUsed)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync(cancellationToken);

            if (resetCode == null)
                return Result.Fail("No OTP found.");

            if (resetCode.Code != code)
                return Result.Fail("Invalid OTP.");

            if (resetCode.ExpiresAt < DateTime.UtcNow)
                return Result.Fail("OTP expired.");

            resetCode.IsUsed = true;
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Ok("OTP verified.");
        }
    }
}

