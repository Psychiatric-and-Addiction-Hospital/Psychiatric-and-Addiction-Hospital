using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.HR.Application;
using Application.Common.Interfaces.HR.CandidatePortal;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Application;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.services.HR.CandidatePortal
{
    public class GetMyApplicationsService : IGetMyApplications
    {
        private readonly AddIdentityDbContext _context;
        private readonly ICurrentUser _currentUser;

        public GetMyApplicationsService(
            AddIdentityDbContext context,
            ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<List<ApplicationResponse>>> GetAsync(
            CancellationToken ct)
        {
            if (!_currentUser.IsAuthenticated)
                return ResponseFactory.Fail<List<ApplicationResponse>>("User must be authenticated.");

            var userId = _currentUser.UserId;

            if (string.IsNullOrWhiteSpace(userId))
                return ResponseFactory.Fail<List<ApplicationResponse>>("Authenticated user must have a valid user ID.");

            var candidate = await _context.Candidates
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.AppUserId == userId, ct);

            if (candidate == null)
                return ResponseFactory.Fail<List<ApplicationResponse>>(
                    "Candidate profile was not found.");

            var applications = await _context.Applications
                .AsNoTracking()
                .Where(x => x.CandidateId == candidate.Id)
                .Include(x => x.JobPosting)
                    .ThenInclude(x => x.Department)
                .Include(x => x.JobPosting)
                    .ThenInclude(x => x.Position)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(ct);

            var response = applications.Select(x => new ApplicationResponse
            {
                Id = x.Id,
                CandidateId = x.CandidateId,
                JobPostingId = x.JobPostingId,
                JobTitle = x.JobPosting.Title,
                DepartmentName = x.JobPosting.Department.Name,
                PositionName = x.JobPosting.Position.Name,
                Status = x.Status,

            }).ToList();

            return ResponseFactory.Success(response, "Applications retrieved successfully.");
        }
    }
}
