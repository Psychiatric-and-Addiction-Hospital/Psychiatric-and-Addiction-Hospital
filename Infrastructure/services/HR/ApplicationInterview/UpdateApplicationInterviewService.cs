using Application.Commands.HR.ApplicationInterview;
using Application.Common.Interfaces.HR.ApplicationInterview;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationInterview;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.ApplicationInterview
{
    public class UpdateApplicationInterviewService : IUpdateApplicationInterview
    {
        private readonly AddIdentityDbContext _context;
        private readonly IApplicationInterviewValidation _validation;

        public UpdateApplicationInterviewService(
            AddIdentityDbContext context,
            IApplicationInterviewValidation validation)
        {
            _context = context;
            _validation = validation;
        }

        public async Task<BaseResponse<ApplicationInterviewResponse>> UpdateAsync(
            UpdateApplicationInterviewCommand request,
            CancellationToken ct)
        {
            var validation = await _validation.ValidateUpdateAsync(request.Request, ct);

            if (!validation.Success)
                return ResponseFactory.Fail<ApplicationInterviewResponse>(validation.Message, validation.Errors);


            var interview = validation.Data!;

            interview.InterviewerId = request.Request.InterviewerId;

            interview.ScheduledAt = request.Request.ScheduledAt;

            interview.DurationInMinutes = request.Request.DurationInMinutes;

            interview.InterviewType = request.Request.InterviewType;

            interview.Location = request.Request.Location?.Trim();

            interview.MeetingLink = request.Request.MeetingLink?.Trim();

            await _context.SaveChangesAsync(ct);

            var updatedInterview = await _context.ApplicationInterviews
                .AsNoTracking()
                .Include(x => x.Interviewer)
                .Include(x => x.Application)
                    .ThenInclude(a => a.Candidate)
                .Include(x => x.Application)
                    .ThenInclude(a => a.JobPosting)
                .FirstAsync(x => x.Id == interview.Id, ct);

            var response = new ApplicationInterviewResponse
            {
                Id = updatedInterview.Id,

                ApplicationId = updatedInterview.ApplicationId,

                InterviewerId = updatedInterview.InterviewerId,

                CandidateName = updatedInterview.Application.Candidate.FullName,

                JobTitle = updatedInterview.Application.JobPosting.Title,

                InterviewerName = updatedInterview.Interviewer.FullName,

                ScheduledAt = updatedInterview.ScheduledAt,

                DurationInMinutes = updatedInterview.DurationInMinutes,

                InterviewType = updatedInterview.InterviewType,

                Status = updatedInterview.Status,

                Result = updatedInterview.Result,

                Score = updatedInterview.Score,

                Location = updatedInterview.Location,

                MeetingLink = updatedInterview.MeetingLink,

                Feedback = updatedInterview.Feedback
            };

            return ResponseFactory.Success(response, "Interview updated successfully.");
        }
    }
}