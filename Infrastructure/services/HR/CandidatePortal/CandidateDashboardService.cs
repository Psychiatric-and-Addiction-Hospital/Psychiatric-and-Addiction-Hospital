using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.HR.CandidatePortal;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Candidate;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.CandidatePortal
{
    public class CandidateDashboardService : ICandidateDashboard
    {
        private readonly AddIdentityDbContext _context;
        private readonly ICurrentUser _currentUser;

        public CandidateDashboardService(AddIdentityDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }
        public async Task<BaseResponse<CandidateDashboardResponse>> GetAsync(CancellationToken ct)
        {
            if (!_currentUser.IsAuthenticated)
            {
                return ResponseFactory.Fail<CandidateDashboardResponse>(
                    "User must be authenticated.");
            }

            var userId = _currentUser.UserId;

            if (string.IsNullOrWhiteSpace(userId))
            {
                return ResponseFactory.Fail<CandidateDashboardResponse>(
                    "Authenticated user must have a valid user ID.");
            }

            var candidate = await _context.Candidates
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.AppUserId == userId,
                    ct);

            if (candidate == null)

                return ResponseFactory.Fail<CandidateDashboardResponse>("Candidate profile was not found.");


            var applications = _context.Applications
            .AsNoTracking()
            .Where(x => x.CandidateId == candidate.Id);

            var dashboard = await applications
                .GroupBy(x => 1)
                .Select(g => new
                {
                    ApplicationsCount = g.Count(),

                    ActiveApplicationsCount = g.Count(x =>
                        x.Status != ApplicationStatus.Hired &&
                        x.Status != ApplicationStatus.Rejected &&
                        x.Status != ApplicationStatus.Withdrawn &&
                        x.Status != ApplicationStatus.OfferDeclined),

                    InterviewsCount = g
                    .SelectMany(x => x.Interviews)
                    .Count(),

                    UpcomingInterviewsCount = g
                    .SelectMany(x => x.Interviews)
                    .Count(x =>
                    x.Status == InterviewStatus.Scheduled &&
                    x.ScheduledAt > DateTime.UtcNow),

                    PendingOffersCount = g.Count(x =>
                        x.Offer != null &&
                        x.Offer.Status == OfferStatus.Pending),

                    AcceptedOffersCount = g.Count(x =>
                        x.Offer != null &&
                        x.Offer.Status == OfferStatus.Accepted),

                    RejectedOffersCount = g.Count(x =>
                        x.Offer != null &&
                        x.Offer.Status == OfferStatus.Rejected)
                })
                .FirstOrDefaultAsync(ct);

            var latestHistory = await applications
                .SelectMany(x => x.StatusHistory)
                .OrderByDescending(x => x.ChangedAt)
                .Select(x => new
                {
                    x.Status,
                    x.ChangedAt
                })
                .FirstOrDefaultAsync(ct);

            var response = new CandidateDashboardResponse
            {
                CandidateName = candidate.FullName,

                ApplicationsCount = dashboard.ApplicationsCount,

                ActiveApplicationsCount = dashboard.ActiveApplicationsCount,

                InterviewsCount = dashboard.InterviewsCount,

                UpcomingInterviewsCount = dashboard.UpcomingInterviewsCount,

                PendingOffersCount = dashboard.PendingOffersCount,

                AcceptedOffersCount = dashboard.AcceptedOffersCount,

                RejectedOffersCount = dashboard.RejectedOffersCount,

                LatestApplicationStatus = latestHistory?.Status,

                LatestApplicationStatusChangedAt = latestHistory?.ChangedAt
            };

            return ResponseFactory.Success(response, "Candidate dashboard retrieved successfully.");
        }
    }
}
