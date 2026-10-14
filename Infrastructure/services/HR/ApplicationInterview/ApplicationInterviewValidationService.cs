using Application.Common.Interfaces.HR.ApplicationInterview;
using Application.Common.Responses;
using Application.DTOS.Request.HR.ApplicationInterview;
using Domain.Entites.HR;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Interview = Domain.Entites.HR.Recruitment.ApplicationInterview;

namespace Infrastructure.services.HR.ApplicationInterview
{
    public class ApplicationInterviewValidationService : IApplicationInterviewValidation
    {
        private readonly AddIdentityDbContext _context;

        public ApplicationInterviewValidationService(AddIdentityDbContext context)
        {
            _context = context;
        }

        #region Create

        public async Task<BaseResponse<bool>> ValidateCreateAsync(CreateApplicationInterviewRequest request, CancellationToken ct)
        {
            if (!await ApplicationExists(request.ApplicationId, ct))
                return ResponseFactory.Fail<bool>("Application not found.");

            var interviewer = await GetInterviewer(request.InterviewerId, ct);

            if (interviewer == null)
                return ResponseFactory.Fail<bool>("Interviewer not found.");

            if (!interviewer.IsActive)
                return ResponseFactory.Fail<bool>("Interviewer is inactive.");

            var dateValidation = ValidateScheduledDate(request.ScheduledAt);
            if (dateValidation != null)
                return dateValidation;

            var durationValidation = ValidateDuration(request.DurationInMinutes);
            if (durationValidation != null)
                return durationValidation;

            var applicationValidation =
                await ValidateApplicationStatus(request.ApplicationId, ct);

            if (applicationValidation != null)
                return applicationValidation;

            var conflictValidation =
                await ValidateInterviewerAvailability(
                    request.InterviewerId,
                    request.ScheduledAt,
                    request.DurationInMinutes,
                    null,
                    ct);

            if (conflictValidation != null)
                return conflictValidation;

            return ResponseFactory.Success(
                true,
                "Validation succeeded.");
        }

        #endregion

        #region Update

        public async Task<BaseResponse<Interview>> ValidateUpdateAsync(UpdateApplicationInterviewRequest request, CancellationToken ct)
        {
            var interview = await _context.ApplicationInterviews
                .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (interview == null)
                return ResponseFactory.Fail<Interview>("Interview not found.");

            var interviewer = await GetInterviewer(request.InterviewerId, ct);

            if (interviewer == null)
                return ResponseFactory.Fail<Interview>("Interviewer not found.");

            if (!interviewer.IsActive)
                return ResponseFactory.Fail<Interview>("Interviewer is inactive.");

            var dateValidation = ValidateScheduledDate(request.ScheduledAt);
            if (dateValidation != null)
                return ResponseFactory.Fail<Interview>(dateValidation.Message);

            var durationValidation = ValidateDuration(request.DurationInMinutes);
            if (durationValidation != null)
                return ResponseFactory.Fail<Interview>(durationValidation.Message);

            var conflictValidation =
                await ValidateInterviewerAvailability(
                    request.InterviewerId,
                    request.ScheduledAt,
                    request.DurationInMinutes,
                    interview.Id,
                    ct);

            if (conflictValidation != null)
                return ResponseFactory.Fail<Interview>(conflictValidation.Message);

            return ResponseFactory.Success(
                interview,
                "Validation succeeded.");
        }

        #endregion

        #region Complete

        public async Task<BaseResponse<Interview>> ValidateCompleteAsync(CompleteInterviewRequest request, CancellationToken ct)
        {
            var interview = await _context.ApplicationInterviews
                .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (interview == null)
                return ResponseFactory.Fail<Interview>("Interview not found.");

            if (interview.Status != InterviewStatus.Scheduled)
                return ResponseFactory.Fail<Interview>("Only scheduled interviews can be completed.");

            if (request.Score < 0 || request.Score > 100)
                return ResponseFactory.Fail<Interview>("Score must be between 0 and 100.");

            return ResponseFactory.Success(interview);
        }

        #endregion

        #region Cancel

        public async Task<BaseResponse<Interview>> ValidateCancelAsync(Guid interviewId, CancellationToken ct)
        {
            var interview = await _context.ApplicationInterviews
                .FirstOrDefaultAsync(x => x.Id == interviewId, ct);

            if (interview == null)
                return ResponseFactory.Fail<Interview>("Interview not found.");

            if (interview.Status != InterviewStatus.Scheduled)
                return ResponseFactory.Fail<Interview>("Interview cannot be cancelled.");

            return ResponseFactory.Success(interview);
        }

        #endregion

        // Helper Methods

        private async Task<bool> ApplicationExists(Guid applicationId, CancellationToken ct)
        {
            return await _context.Applications
                .AnyAsync(x => x.Id == applicationId, ct);
        }

        private async Task<Domain.Entites.HR.Employee?> GetInterviewer(Guid interviewerId, CancellationToken ct)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(x => x.Id == interviewerId, ct);
        }

        private BaseResponse<bool>? ValidateScheduledDate(DateTime scheduledAt)
        {
            if (scheduledAt <= DateTime.UtcNow)
                return ResponseFactory.Fail<bool>("Interview date must be in the future.");

            return null;
        }

        private BaseResponse<bool>? ValidateDuration(int duration)
        {
            if (duration <= 0)
                return ResponseFactory.Fail<bool>("Duration must be greater than zero.");

            return null;
        }

        private async Task<BaseResponse<bool>?> ValidateApplicationStatus(Guid applicationId, CancellationToken ct)
        {
            var status = await _context.Applications
                .Where(x => x.Id == applicationId)
                .Select(x => x.Status)
                .FirstAsync(ct);

            if (status == ApplicationStatus.Rejected ||
                status == ApplicationStatus.Withdrawn ||
                status == ApplicationStatus.Hired)
            {
                return ResponseFactory.Fail<bool>(
                    $"Cannot schedule interview for application with status '{status}'.");
            }

            return null;
        }

        private async Task<BaseResponse<bool>?> ValidateInterviewerAvailability
            (Guid interviewerId, DateTime scheduledAt, int duration, Guid? interviewId, CancellationToken ct)
        {
            var endTime = scheduledAt.AddMinutes(duration);

            var hasConflict = await _context.ApplicationInterviews.AnyAsync(x =>
                x.InterviewerId == interviewerId &&
                x.Status == InterviewStatus.Scheduled &&
                (!interviewId.HasValue || x.Id != interviewId.Value) &&
                scheduledAt < x.ScheduledAt.AddMinutes(x.DurationInMinutes) &&
                endTime > x.ScheduledAt,
                ct);

            if (hasConflict)
            {
                return ResponseFactory.Fail<bool>(
                    "The interviewer already has another interview scheduled during this time.");
            }

            return null;
        }
    }
}