using Application.Common.Interfaces.HR.Dashboard;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Dashboard;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.services.HR.Dashboard
{
    public class GetRecruitmentDashboardService : IGetRecruitmentDashboard
    {
        private readonly AddIdentityDbContext _context;

        public GetRecruitmentDashboardService(
            AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<RecruitmentDashboardResponse>> GetAsync(CancellationToken ct)
        {
            var JobPostings = await _context.JobPostings
                .AsNoTracking()
                .GroupBy(x => x.Status)
                .Select(g => new
                {
                    Status = g.Key,
                    Count = g.Count()
                })
                .ToListAsync(ct);
            var jobPostingsByStatus = JobPostings.ToDictionary(x => x.Status, x => x.Count);

            var publishedJobPostings = jobPostingsByStatus.GetValueOrDefault(JobPostingStatus.Published);
            var closedJobPostings = jobPostingsByStatus.GetValueOrDefault(JobPostingStatus.Closed);

            var Application = await _context.Applications
                .AsNoTracking()
                .GroupBy(x => x.Status)
                .Select(g => new
                {
                    Status = g.Key,
                    Count = g.Count()
                })
                .ToListAsync(ct);

            var applicationsByStatus = Application.ToDictionary(x => x.Status, x => x.Count);

            var applicationsReceived = applicationsByStatus.Values.Sum();
            var offersSent = applicationsByStatus.GetValueOrDefault(ApplicationStatus.Offered);
            var hiredCandidates = applicationsByStatus.GetValueOrDefault(ApplicationStatus.Hired);


            var interviewsScheduled =
                await _context.ApplicationInterviews
                    .AsNoTracking()
                    .CountAsync(x => x.Status == InterviewStatus.Scheduled, ct);

            var totalCandidates =
              await _context.Candidates
                  .AsNoTracking()
                  .CountAsync(ct);

            var response = new RecruitmentDashboardResponse
            {
                PublishedJobPostings = publishedJobPostings,

                ClosedJobPostings = closedJobPostings,

                TotalCandidates = totalCandidates,

                ApplicationsReceived = applicationsReceived,

                InterviewsScheduled = interviewsScheduled,

                OffersSent = offersSent,

                HiredCandidates = hiredCandidates
            };

            return ResponseFactory.Success(response, "Recruitment dashboard retrieved successfully.");
        }
    }
}
