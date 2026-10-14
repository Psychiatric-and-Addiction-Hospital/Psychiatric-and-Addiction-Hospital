using Application.Common.Constants;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.HR.CandidatePortal;
using Application.Common.Responses;
using Application.DTOS.Request.HR.Candidate;
using Application.DTOS.Responses.HR.Candidate;
using Domain.Entites;
using Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.services.HR.CandidatePortal
{
    public class CreateCandidateAccountService : ICreateCandidateAccount
    {

        private readonly AddIdentityDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailVerificationService _emailVerificationService;
        private readonly ICandidateAccountTokenService _candidateAccountTokenService;

        public CreateCandidateAccountService(AddIdentityDbContext context,
            UserManager<AppUser> userManager, IEmailVerificationService emailVerificationService,
            ICandidateAccountTokenService candidateAccountTokenService)
        {
            _context = context;
            _userManager = userManager;
            _emailVerificationService = emailVerificationService;
            _candidateAccountTokenService = candidateAccountTokenService;
        }
        public async Task<BaseResponse<CandidateAccountResponse>> CreateAsync(CreateCandidateAccountRequest request, CancellationToken ct)
        {
            var candidate = await _context.Candidates.FirstOrDefaultAsync(x => x.Id == request.CandidateId, ct);

            if (candidate == null)
                return ResponseFactory.Fail<CandidateAccountResponse>("Candidate was not found.");


            if (!string.IsNullOrWhiteSpace(candidate.AppUserId))
                return ResponseFactory.Fail<CandidateAccountResponse>("Candidate already has an account.");

            var tokenIsValid = _candidateAccountTokenService.ValidateToken(request.Token, candidate.Id, candidate.Email);

            if (!tokenIsValid)
                return ResponseFactory.Fail<CandidateAccountResponse>("Invalid or expired account setup link.");


            var existingUser = await _userManager.FindByEmailAsync(candidate.Email);

            if (existingUser != null)
                return ResponseFactory.Fail<CandidateAccountResponse>("An account already exists with this email.");

            var user = new AppUser
            {
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                UserName = candidate.Email,
                Email = candidate.Email,
                PhoneNumber = candidate.PhoneNumber,
                Address = candidate.Address,
                Gender = candidate.Gender,
                ImageUrl = candidate.Image,
                IsActive = true,
                EmailConfirmed = false
            };

            var createResult = await _userManager.CreateAsync(user, request.Password);

            if (!createResult.Succeeded)
            {
                var errors = createResult.Errors
                    .Select(x => x.Description)
                    .ToList();
                return ResponseFactory.Fail<CandidateAccountResponse>(
               "Failed to create candidate account.",
               errors);
            }

            var roleResult = await _userManager.AddToRoleAsync(user, Roles.Candidate);

            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);

                var errors = roleResult.Errors
                    .Select(x => x.Description)
                    .ToList();

                return ResponseFactory.Fail<CandidateAccountResponse>(
                    "Failed to assign candidate role.",
                    errors);
            }

            candidate.AppUserId = user.Id;

            await _context.SaveChangesAsync(ct);

            var verificationResult = await _emailVerificationService.SendVerificationEmailAsync(user.Id, ct);

            if (!verificationResult.Success)
                return ResponseFactory.Fail<CandidateAccountResponse>
                    ("Account was created, but verification email could not be sent.", verificationResult.Errors);


            return ResponseFactory.Success(
                new CandidateAccountResponse
                {
                    CandidateId = candidate.Id,
                    UserId = user.Id,
                    Email = user.Email!,
                    EmailConfirmed = user.EmailConfirmed,
                    Message = verificationResult.Success
                ? "Candidate account created successfully. Please verify your email."
                : "Candidate account created successfully, but verification email could not be sent. Please request another verification email."
                },
                "Candidate account created successfully.");

        }
    }
}
