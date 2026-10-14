using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.HR.CandidatePortal;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Candidate;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.services.HR.CandidatePortal
{
    internal class CandidateInterviewService : ICandidateInterview
    {
        private readonly ICurrentUser _currentUser;
        private readonly AddIdentityDbContext _context;
        public CandidateInterviewService(ICurrentUser currentUser, AddIdentityDbContext context)
        {
            _currentUser = currentUser;
            _context = context;
        }
        public async Task<BaseResponse<List<CandidateInterviewResponse>>> GetUpcomingAsync(CancellationToken ct)
        {
            if (!_currentUser.IsAuthenticated)
            {
                return ResponseFactory.Fail<List<CandidateInterviewResponse>>(
                    "User must be authenticated.");
            }

            var userId = _currentUser.UserId;

            if (string.IsNullOrWhiteSpace(userId))
            {
                return ResponseFactory.Fail<List<CandidateInterviewResponse>>(
                    "Authenticated user must have a valid user ID.");
            }

            var interviews = await _context.ApplicationInterviews
                .AsNoTracking()
                .Where(x =>
                    x.Application.Candidate.AppUserId == userId &&
                    x.Status == InterviewStatus.Scheduled &&
                    x.ScheduledAt > DateTime.UtcNow)
                .OrderBy(x => x.ScheduledAt)
                .Select(x => new CandidateInterviewResponse
                {
                    Id = x.Id,

                    ApplicationId = x.ApplicationId,

                    JobTitle = x.Application.JobPosting.Title,

                    PositionName = x.Application.JobPosting.Position.Name,

                    DepartmentName = x.Application.JobPosting.Department.Name,

                    InterviewerName = x.Interviewer.FullName,
                    ScheduledAt = x.ScheduledAt,

                    DurationInMinutes = x.DurationInMinutes,

                    InterviewType = x.InterviewType,

                    Status = x.Status,

                    Location = x.Location,

                    MeetingLink = x.MeetingLink
                })
                .ToListAsync(ct);

            return ResponseFactory.Success(interviews, "Upcoming interviews retrieved successfully.");
        }
    }
}
