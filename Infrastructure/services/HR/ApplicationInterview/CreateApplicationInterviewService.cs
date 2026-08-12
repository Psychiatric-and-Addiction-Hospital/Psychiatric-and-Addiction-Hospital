using Application.Commands.HR.ApplicationInterview;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.HR.ApplicationInterview;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationInterview;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.ApplicationInterview
{
    public class CreateApplicationInterviewService : ICreateApplicationInterview
    {
        private readonly AddIdentityDbContext _context;
        private readonly IApplicationInterviewValidation _validation;
        private readonly IInterviewInvitationEmailService _emailService;

        public CreateApplicationInterviewService(
            AddIdentityDbContext context,
            IApplicationInterviewValidation validation,
            IInterviewInvitationEmailService emailService)
        {
            _context = context;
            _validation = validation;
            _emailService = emailService;
        }

        public async Task<BaseResponse<ApplicationInterviewResponse>> CreateAsync(CreateApplicationInterviewCommand request, CancellationToken ct)
        {

            var validation = await _validation
                .ValidateCreateAsync(request.Request, ct);

            if (!validation.Success)
            {
                return ResponseFactory.Fail<ApplicationInterviewResponse>(
                    validation.Message,
                    validation.Errors);
            }

            var interview = new Domain.Entites.HR.Recruitment.ApplicationInterview
            {
                ApplicationId = request.Request.ApplicationId,

                InterviewerId = request.Request.InterviewerId,

                ScheduledAt = request.Request.ScheduledAt,

                DurationInMinutes = request.Request.DurationInMinutes,

                InterviewType = request.Request.InterviewType,

                Status = InterviewStatus.Scheduled,

                Location = request.Request.Location?.Trim(),

                MeetingLink = request.Request.MeetingLink?.Trim()
            };

            _context.ApplicationInterviews.Add(interview);

            await _context.SaveChangesAsync(ct);

            var createdInterview = await _context.ApplicationInterviews
                .Include(x => x.Interviewer)
                .Include(x => x.Application)
                     .ThenInclude(a => a.Candidate)
                .Include(x => x.Application)
                     .ThenInclude(a => a.JobPosting)
                          .ThenInclude(j => j.Position)
                .Include(x => x.Application)
                     .ThenInclude(a => a.JobPosting)
                          .ThenInclude(j => j.Department)
                .FirstAsync(x => x.Id == interview.Id, ct);

            await _emailService.SendAsync(createdInterview.Application.Candidate, createdInterview, ct);

            var response = new ApplicationInterviewResponse
            {
                Id = createdInterview.Id,

                ApplicationId = createdInterview.ApplicationId,

                CandidateName = createdInterview.Application.Candidate.FullName,

                JobTitle = createdInterview.Application.JobPosting.Title,

                InterviewerId = createdInterview.InterviewerId,

                InterviewerName = createdInterview.Interviewer.FullName,

                ScheduledAt = createdInterview.ScheduledAt,

                DurationInMinutes = createdInterview.DurationInMinutes,

                InterviewType = createdInterview.InterviewType,

                Status = createdInterview.Status,

                Result = createdInterview.Result,

                Score = createdInterview.Score,

                Location = createdInterview.Location,

                MeetingLink = createdInterview.MeetingLink,

                Feedback = createdInterview.Feedback
            };

            return ResponseFactory.Success(response, "Interview scheduled successfully.");
        }
    }
}

