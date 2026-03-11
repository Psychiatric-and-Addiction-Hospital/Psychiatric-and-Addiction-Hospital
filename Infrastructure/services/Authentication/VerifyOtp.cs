using Application.Common.Interfaces.Authentication;
using Application.Common.Responses;
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

        public async Task<BaseResponse<string>> VerifyOtpAsync(string email, string code, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return ResponseFactory.Fail<string>("Invalid Email.");

            var resetCode = await _context.PasswordResetCodes
                .Where(x => x.UserId == user.Id && !x.IsUsed)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync(cancellationToken);

            if (resetCode == null)
                return ResponseFactory.Fail<string>("No OTP found.");

            if (resetCode.Code != code)
                return ResponseFactory.Fail<string>("Invalid OTP.");

            if (resetCode.ExpiresAt < DateTime.UtcNow)
                return ResponseFactory.Fail<string>("OTP Expired.");
            

            resetCode.IsUsed = true;
            await _context.SaveChangesAsync(cancellationToken);

            return ResponseFactory.Success("OTP verified..");

        }
    }
}

