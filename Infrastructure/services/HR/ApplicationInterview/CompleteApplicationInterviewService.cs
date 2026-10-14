using Application.Commands.HR.ApplicationInterview;
using Application.Common.Interfaces.HR.ApplicationInterview;
using Application.Common.Responses;
using Application.DTOS.Request.HR.ApplicationInterview;
using Application.DTOS.Responses.HR.ApplicationInterview;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.services.HR.ApplicationInterview
{
    public class CompleteApplicationInterviewService : ICompleteApplicationInterview
    {
        private readonly AddIdentityDbContext _context;
        private readonly IApplicationInterviewValidation _validation;

        public CompleteApplicationInterviewService(AddIdentityDbContext context, IApplicationInterviewValidation validation)
        {
            _context = context;
            _validation = validation;
        }

        public async Task<BaseResponse<ApplicationInterviewResponse>> CompleteAsync(
            CompleteInterviewRequest request,
            CancellationToken ct)
        {
            var validation = await _validation
                .ValidateCompleteAsync(request, ct);

            if (!validation.Success)
                return ResponseFactory.Fail<ApplicationInterviewResponse>(validation.Message, validation.Errors);

            var interview = validation.Data!;

            interview.Status = InterviewStatus.Completed;

            interview.Result = request.Result;

            interview.Score = request.Score;

            interview.Feedback = request.Feedback?.Trim();


            await _context.SaveChangesAsync(ct);

            var completedInterview = await _context.ApplicationInterviews
                .AsNoTracking()
                .Include(x => x.Interviewer)
                .Include(x => x.Application)
                    .ThenInclude(a => a.Candidate)
                .Include(x => x.Application)
                    .ThenInclude(a => a.JobPosting)
                .FirstAsync(x => x.Id == interview.Id, ct);

            var response = new ApplicationInterviewResponse
            {
                Id = completedInterview.Id,

                ApplicationId = completedInterview.ApplicationId,

                InterviewerId = completedInterview.InterviewerId,

                CandidateName = completedInterview.Application.Candidate.FullName,

                JobTitle = completedInterview.Application.JobPosting.Title,

                InterviewerName = completedInterview.Interviewer.FullName,

                ScheduledAt = completedInterview.ScheduledAt,

                DurationInMinutes = completedInterview.DurationInMinutes,

                InterviewType = completedInterview.InterviewType,

                Status = completedInterview.Status,

                Result = completedInterview.Result,

                Score = completedInterview.Score,

                Location = completedInterview.Location,

                MeetingLink = completedInterview.MeetingLink,

                Feedback = completedInterview.Feedback
            };

            return ResponseFactory.Success(response, "Interview completed successfully.");
        }
    }
}

