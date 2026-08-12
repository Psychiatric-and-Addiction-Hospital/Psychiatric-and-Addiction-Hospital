using Application.Common.Interfaces.Authentication;
using Application.Common.Responses;
using Application.DTOS.Responses.Authentication;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Infrastructure.services.Authentication
{
    public class EmailVerificationService : IEmailVerificationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public EmailVerificationService(UserManager<AppUser> userManager, IEmailService emailService, IConfiguration configuration)
        {
            _userManager = userManager;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<BaseResponse<bool>> SendVerificationEmailAsync(string userId, CancellationToken ct)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return ResponseFactory.Fail<bool>("User account was not found.");

            if (user.EmailConfirmed)
                return ResponseFactory.Success(true, "Email is already verified.");


            var frontendUrl = _configuration["Frontend:EmailVerificationUrl"];

            if (string.IsNullOrWhiteSpace(frontendUrl))
                return ResponseFactory.Fail<bool>("Email verification URL is not configured.");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var verificationUrl = $"{frontendUrl}?userId={userId}&token={encodedToken}";

            var fullName = $"{user.FirstName} {user.LastName}".Trim();

            var subject = "Verify your email - Psychiatric & Addiction Hospital";

            var body = BuildEmailBody(fullName, verificationUrl);

            await _emailService.SendAsync(user.Email, subject, body);

            return ResponseFactory.Success(true, "Verification email sent successfully.");
        }

        public async Task<BaseResponse<EmailVerificationResponse>> VerifyEmailAsync(string userId, string token, CancellationToken ct)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return ResponseFactory.Fail<EmailVerificationResponse>("User account was not found.");

            if (user.EmailConfirmed)
                return ResponseFactory.Success(new EmailVerificationResponse { IsVerified = true }, "Email is already verified.");

            string decodedToken;

            try
            {
                decodedToken = Encoding.UTF8.GetString(
                    WebEncoders.Base64UrlDecode(token));
            }
            catch
            {
                return ResponseFactory.Fail<EmailVerificationResponse>("Invalid verification token.");
            }

            var result =
                await _userManager.ConfirmEmailAsync(user, decodedToken);

            if (!result.Succeeded)
            {
                var errors = result.Errors
                    .Select(x => x.Description)
                    .ToList();

                return ResponseFactory.Fail<EmailVerificationResponse>("Email verification failed.", errors);
            }

            return ResponseFactory.Success(new EmailVerificationResponse { IsVerified = true }, "Email verified successfully.");
        }

        public async Task<BaseResponse<bool>> ResendVerificationEmailAsync(string email,CancellationToken ct)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return ResponseFactory.Fail<bool>("User account was not found.");

            if (user.EmailConfirmed)
                return ResponseFactory.Success(true,"Email is already verified.");

            var frontendUrl =_configuration["Frontend:EmailVerificationUrl"];

            if (string.IsNullOrWhiteSpace(frontendUrl))
                return ResponseFactory.Fail<bool>("Email verification URL is not configured.");

            var token =await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var encodedToken =WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var verificationUrl =
                $"{frontendUrl}?userId={user.Id}&token={encodedToken}";

            var fullName =
                $"{user.FirstName} {user.LastName}".Trim();

            var subject =
                "Verify your email - Psychiatric & Addiction Hospital";

            var body =
                BuildEmailBody(fullName, verificationUrl);

            await _emailService.SendAsync(
                user.Email!,
                subject,
                body);

            return ResponseFactory.Success(
                true,
                "Verification email sent successfully.");
        }

        private static string BuildEmailBody(string fullName, string verificationUrl)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">
</head>

<body style=""
    margin:0;
    padding:0;
    background-color:#f4f6f8;
    font-family:Arial,Helvetica,sans-serif;
"">

    <div style=""
        max-width:600px;
        margin:40px auto;
        background:#ffffff;
        border-radius:10px;
        overflow:hidden;
    "">

        <div style=""
            background-color:#2B3A67;
            padding:30px;
            text-align:center;
            color:white;
        "">
            <h1>
                Psychiatric & Addiction Hospital
            </h1>
        </div>

        <div style=""padding:35px;color:#333333;"">

            <h2 style=""color:#2B3A67;"">
                Welcome, {fullName}!
            </h2>

            <p style=""font-size:16px;line-height:1.7;"">
                Thank you for creating an account with
                Psychiatric & Addiction Hospital.
            </p>

            <p style=""font-size:16px;line-height:1.7;"">
                Please verify your email address to activate
                your account.
            </p>

            <div style=""text-align:center;margin:30px 0;"">

                <a href=""{verificationUrl}""
                   style=""
                       display:inline-block;
                       padding:14px 30px;
                       background-color:#2B3A67;
                       color:#ffffff;
                       text-decoration:none;
                       border-radius:6px;
                       font-size:16px;
                       font-weight:bold;
                   "">
                    Verify Email
                </a>

            </div>

            <p style=""font-size:14px;color:#777777;"">
                If you did not create this account,
                you can safely ignore this email.
            </p>

            <p style=""margin-top:30px;"">
                Best regards,<br>
                <strong>
                    Psychiatric & Addiction Hospital
                </strong>
            </p>

        </div>

    </div>

</body>
</html>";
        }
    }
}
