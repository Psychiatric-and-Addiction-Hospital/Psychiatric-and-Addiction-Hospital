using Application.Common.Interfaces.HR.Application;
using Application.Common.Responses;
using Application.DTOS.Request.HR.Application;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using applicationEntity = Domain.Entites.HR.Recruitment.Application;

namespace Infrastructure.services.HR.Application
{
    public class ApplicationValidationService : IApplicationValidation
    {
        private readonly AddIdentityDbContext _context;

        public ApplicationValidationService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<bool>> ValidateApplyAsync(CreateApplicationRequest request, CancellationToken ct)
        {
            var candidate = await _context.Candidates
                .FirstOrDefaultAsync(x => x.Id == request.CandidateId, ct);

            if (candidate == null)
                return ResponseFactory.Fail<bool>("Candidate not found.");

            if (!candidate.IsActive)
                return ResponseFactory.Fail<bool>("Candidate is inactive.");

            var jobPosting = await _context.JobPostings
                .FirstOrDefaultAsync(x => x.Id == request.JobPostingId, ct);

            if (jobPosting == null)
                return ResponseFactory.Fail<bool>("Job posting not found.");

            if (jobPosting.Status != JobPostingStatus.Published)
                return ResponseFactory.Fail<bool>("Job posting is not published.");

            if (jobPosting.ClosingDate < DateTime.UtcNow)
                return ResponseFactory.Fail<bool>("Job posting has expired.");

            if (jobPosting.Vacancies <= 0)
                return ResponseFactory.Fail<bool>("No vacancies available.");

            var alreadyApplied = await _context.Applications.AnyAsync(
                x => x.CandidateId == request.CandidateId &&
                     x.JobPostingId == request.JobPostingId,
                ct);

            if (alreadyApplied)
                return ResponseFactory.Fail<bool>("Candidate has already applied.");

            return ResponseFactory.Success(true, "Validation succeeded.");
        }

        public async Task<BaseResponse<applicationEntity>> ValidateStatusUpdateAsync(
            Guid applicationId,
            CancellationToken ct)
        {
            var application = await _context.Applications
                .Include(x => x.Candidate)
                .Include(x => x.JobPosting)
                .FirstOrDefaultAsync(x => x.Id == applicationId, ct);

            if (application == null)
                return ResponseFactory.Fail<applicationEntity>("Application not found.");

            return ResponseFactory.Success(application, "Validation succeeded.");
        }

        public async Task<BaseResponse<applicationEntity>> ValidateStatusTransitionAsync(Guid applicationId, ApplicationStatus newStatus, CancellationToken ct)
        {
            var application = await _context.Applications
                .Include(x => x.Candidate)
                .Include(x => x.JobPosting)
                .ThenInclude(x => x.Department)
                .Include(x => x.JobPosting)
                .ThenInclude(x => x.Position)
                .FirstOrDefaultAsync(x => x.Id == applicationId, ct);

            if (application == null)
                return ResponseFactory.Fail<applicationEntity>("Application not found.");

            if (!IsValidStatusTransition(application.Status, newStatus))
            {
                return ResponseFactory.Fail<applicationEntity>(
                    $"Cannot change status from {application.Status} to {newStatus}.");
            }

            return ResponseFactory.Success(application, "Validation succeeded.");
        }

        public async Task<BaseResponse<applicationEntity>> ValidateDeleteAsync(Guid applicationId, CancellationToken ct)
        {
            var application = await _context.Applications
                .FirstOrDefaultAsync(x => x.Id == applicationId, ct);

            if (application == null)
                return ResponseFactory.Fail<applicationEntity>("Application not found.");

            if (application.Status == ApplicationStatus.Hired)
                return ResponseFactory.Fail<applicationEntity>(
                    "Hired applications cannot be deleted.");

            return ResponseFactory.Success(application, "Validation succeeded.");
        }


        private static bool IsValidStatusTransition(ApplicationStatus currentStatus, ApplicationStatus newStatus)
        {
            return currentStatus switch
            {
                ApplicationStatus.Pending =>
                    newStatus == ApplicationStatus.UnderReview ||
                    newStatus == ApplicationStatus.Rejected ||
                    newStatus == ApplicationStatus.Withdrawn,

                ApplicationStatus.UnderReview =>
                    newStatus == ApplicationStatus.InterviewScheduled ||
                    newStatus == ApplicationStatus.Rejected ||
                    newStatus == ApplicationStatus.Withdrawn,

                ApplicationStatus.InterviewScheduled =>
                    newStatus == ApplicationStatus.InterviewCompleted ||
                    newStatus == ApplicationStatus.Rejected ||
                    newStatus == ApplicationStatus.Withdrawn,

                ApplicationStatus.InterviewCompleted =>
                    newStatus == ApplicationStatus.Offered ||
                    newStatus == ApplicationStatus.Rejected,

                ApplicationStatus.Offered =>
                    newStatus == ApplicationStatus.Hired ||
                    newStatus == ApplicationStatus.Withdrawn,

                ApplicationStatus.Hired => false,

                ApplicationStatus.Rejected => false,

                ApplicationStatus.Withdrawn => false,

                _ => false
            };
        }

    }
}
