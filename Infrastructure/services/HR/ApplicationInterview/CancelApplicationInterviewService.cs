using Application.Commands.HR.ApplicationInterview;
using Application.Common.Interfaces.HR.ApplicationInterview;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationInterview;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.ApplicationInterview
{
    public class CancelApplicationInterviewService : ICancelApplicationInterview
    {
        private readonly AddIdentityDbContext _context;
        private readonly IApplicationInterviewValidation _validation;

        public CancelApplicationInterviewService(AddIdentityDbContext context, IApplicationInterviewValidation validation)
        {
            _context = context;
            _validation = validation;
        }

        public async Task<BaseResponse<ApplicationInterviewResponse>> CancelAsync(Guid InterviewId, CancellationToken ct)
        {
            var validation = await _validation.ValidateCancelAsync(InterviewId, ct);

            if (!validation.Success)
                return ResponseFactory.Fail<ApplicationInterviewResponse>(validation.Message, validation.Errors);

            var interview = validation.Data!;

            interview.Status = InterviewStatus.Cancelled;

            await _context.SaveChangesAsync(ct);

            var cancelledInterview = await _context.ApplicationInterviews
                .AsNoTracking()
                .Include(x => x.Interviewer)
                .Include(x => x.Application)
                    .ThenInclude(a => a.Candidate)
                .Include(x => x.Application)
                    .ThenInclude(a => a.JobPosting)
                .FirstAsync(x => x.Id == interview.Id, ct);

            var response = new ApplicationInterviewResponse
            {
                Id = cancelledInterview.Id,

                ApplicationId = cancelledInterview.ApplicationId,

                InterviewerId = cancelledInterview.InterviewerId,

                CandidateName = cancelledInterview.Application.Candidate.FullName,

                JobTitle = cancelledInterview.Application.JobPosting.Title,

                InterviewerName = cancelledInterview.Interviewer.FullName,

                ScheduledAt = cancelledInterview.ScheduledAt,

                DurationInMinutes = cancelledInterview.DurationInMinutes,

                InterviewType = cancelledInterview.InterviewType,

                Status = cancelledInterview.Status,

                Result = cancelledInterview.Result,

                Score = cancelledInterview.Score,

                Location = cancelledInterview.Location,

                MeetingLink = cancelledInterview.MeetingLink,

                Feedback = cancelledInterview.Feedback
            };

            return ResponseFactory.Success(
                response,
                "Interview cancelled successfully.");
        }
    }
}
