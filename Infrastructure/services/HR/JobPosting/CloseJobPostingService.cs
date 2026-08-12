using Application.Common.Interfaces.HR.JobPosting;
using Application.Common.Responses;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;

namespace Infrastructure.services.HR.JobPosting
{
    public class CloseJobPostingService : ICloseJobPosting
    {
        private readonly AddIdentityDbContext _context;
        private readonly IJobPostingValidation _validation;

        public CloseJobPostingService(
            AddIdentityDbContext context,
            IJobPostingValidation validation)
        {
            _context = context;
            _validation = validation;
        }

        public async Task<BaseResponse<bool>> CloseAsync(Guid jobPostingId, CancellationToken ct)
        {
            var validation = await _validation.ValidateStatusChangeAsync(jobPostingId, ct);

            if (!validation.Success)
                return ResponseFactory.Fail<bool>(validation.Message, validation.Errors);

            var jobPosting = validation.Data!;

            if (jobPosting.Status == JobPostingStatus.Closed)
                return ResponseFactory.Fail<bool>("Job posting is already closed.");

            jobPosting.Status = JobPostingStatus.Closed;

            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(true, "Job posting closed successfully.");
        }
    }
}

