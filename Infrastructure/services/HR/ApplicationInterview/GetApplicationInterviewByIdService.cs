using Application.Common.Interfaces.HR.ApplicationInterview;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationInterview;
using Application.Queries.HR.ApplicationInterview;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.ApplicationInterview
{
    public class GetApplicationInterviewByIdService : IGetApplicationInterviewById
    {
        private readonly AddIdentityDbContext _context;

        public GetApplicationInterviewByIdService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<ApplicationInterviewResponse>> GetByIdAsync(GetApplicationInterviewByIdQuery request, CancellationToken ct)
        {
            var interview = await _context.ApplicationInterviews
               .AsNoTracking()
               .Include(x => x.Interviewer)
               .Include(x => x.Application)
                   .ThenInclude(a => a.Candidate)
               .Include(x => x.Application)
                   .ThenInclude(a => a.JobPosting)
               .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (interview == null)
                return ResponseFactory.Fail<ApplicationInterviewResponse>("Interview not found.");

            var response = new ApplicationInterviewResponse
            {
                Id = interview.Id,

                ApplicationId = interview.ApplicationId,

                InterviewerId = interview.InterviewerId,

                CandidateName = interview.Application.Candidate.FullName,

                JobTitle = interview.Application.JobPosting.Title,

                InterviewerName = interview.Interviewer.FullName,

                ScheduledAt = interview.ScheduledAt,

                DurationInMinutes = interview.DurationInMinutes,

                InterviewType = interview.InterviewType,

                Status = interview.Status,

                Result = interview.Result,

                Score = interview.Score,

                Location = interview.Location,

                MeetingLink = interview.MeetingLink,

                Feedback = interview.Feedback
            };

            return ResponseFactory.Success(response, "Interview retrieved successfully.");
        }
    }
}

