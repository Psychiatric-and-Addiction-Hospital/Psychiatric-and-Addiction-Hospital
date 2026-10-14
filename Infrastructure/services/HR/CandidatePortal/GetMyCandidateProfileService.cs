using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.HR.CandidatePortal;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Candidate;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.CandidatePortal
{
    public class GetMyCandidateProfileService : IGetMyCandidateProfile
    {
        private readonly AddIdentityDbContext _context;
        private readonly ICurrentUser _currentUser;

        public GetMyCandidateProfileService(
            AddIdentityDbContext context,
            ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<CandidateResponse>> GetAsync(CancellationToken ct)
        {
            if (!_currentUser.IsAuthenticated)
                return ResponseFactory.Fail<CandidateResponse>("User is not authenticated.");


            var userId = _currentUser.UserId;

            if (string.IsNullOrWhiteSpace(userId))
                return ResponseFactory.Fail<CandidateResponse>("Current user was not found.");


            var candidate = await _context.Candidates
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.AppUserId == userId, ct);

            if (candidate == null)
                return ResponseFactory.Fail<CandidateResponse>("Candidate profile was not found.");


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

            return ResponseFactory.Success(response, "Candidate profile retrieved successfully.");
        }
    }
}