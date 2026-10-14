using Application.Commands.HR.Candidate;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.HR.Candidate;
using Application.Common.Interfaces.UpLoad;
using Application.Common.Responses;
using Application.DTOS.Request.HR.Candidate;
using Application.DTOS.Responses.HR.Candidate;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.services.HR.Candidate
{
    public class CreateCandidateService : ICreateCandidate
    {
        private readonly AddIdentityDbContext _context;

        private readonly ICandidateAccountTokenService _candidateAccountTokenService;

        private readonly ICandidateAccountEmailService _candidateAccountEmailService;

        private readonly IConfiguration _configuration;

        private readonly IFileStorage _fileStorage;
        public CreateCandidateService(AddIdentityDbContext context, IFileStorage fileStorage,
            ICandidateAccountTokenService candidateAccountTokenService,
            ICandidateAccountEmailService candidateAccountEmailService, IConfiguration configuration)
        {
            _context = context;
            _fileStorage = fileStorage;
            _candidateAccountTokenService = candidateAccountTokenService;
            _candidateAccountEmailService = candidateAccountEmailService;
            _configuration = configuration;
        }

        public async Task<BaseResponse<CandidateResponse>> CreateAsync(CreateCandidateRequest request, CancellationToken ct)
        {
            var exists = await _context.Candidates.AnyAsync(x => x.Email == request.Email, ct);

            if (exists)
                return ResponseFactory.Fail<CandidateResponse>("A candidate with this email already exists.");

            string? imageUrl = null;

            if (request.Image != null)
                imageUrl = await _fileStorage.SaveFileAsync(request.Image, "candidate-images", ct);

            string? resumeUrl = null;

            if (request.Resume != null)
                resumeUrl = await _fileStorage.SaveFileAsync(request.Resume, "candidate-resumes", ct);

            var candidate = new Domain.Entites.HR.Recruitment.Candidate
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                NationalId = request.NationalId,
                Gender = request.Gender,
                Address = request.Address,
                DateOfBirth = request.DateOfBirth,
                YearsOfExperience = request.YearsOfExperience,
                CurrentCompany = request.CurrentCompany,
                CurrentPosition = request.CurrentPosition,
                CurrentSalary = request.CurrentSalary,
                ExpectedSalary = request.ExpectedSalary,
                LinkedInUrl = request.LinkedInUrl,
                ResumeUrl = resumeUrl,
                Image = imageUrl,
                Notes = request.Notes,
                IsActive = true
            };

            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync(ct);

            var token = _candidateAccountTokenService.GenerateToken(candidate.Id, candidate.Email);

            var frontendUrl = _configuration["Frontend:CandidateAccountSetupUrl"];

            if (string.IsNullOrWhiteSpace(frontendUrl))
                return ResponseFactory.Fail<CandidateResponse>("Candidate account setup URL is not configured.");

            var setupUrl = $"{frontendUrl}" + $"?candidateId={candidate.Id}" + $"&token={Uri.EscapeDataString(token)}";

            await _candidateAccountEmailService.SendAsync(candidate.Email, candidate.FullName, setupUrl, ct);

            var response = new CandidateResponse
            {
                Id = candidate.Id,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                FullName = candidate.FullName,
                Email = candidate.Email,
                PhoneNumber = candidate.PhoneNumber,
                DateOfBirth = candidate.DateOfBirth,
                YearsOfExperience = candidate.YearsOfExperience,
                CurrentCompany = candidate.CurrentCompany,
                CurrentPosition = candidate.CurrentPosition,
                CurrentSalary = candidate.CurrentSalary,
                ExpectedSalary = candidate.ExpectedSalary,
                LinkedInUrl = candidate.LinkedInUrl,
                ResumeUrl = candidate.ResumeUrl,
                ImageUrl = candidate.Image,
                Notes = candidate.Notes,
                IsActive = candidate.IsActive
            };

            return ResponseFactory.Success(response, "Candidate created successfully.");

        }
    }
}
