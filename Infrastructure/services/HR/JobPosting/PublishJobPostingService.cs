using Application.Common.Interfaces.HR.JobPosting;
using Application.Common.Responses;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.JobPosting
{
    public class PublishJobPostingService : IPublishJobPosting

    {
        private readonly AddIdentityDbContext _context;
        private readonly IJobPostingValidation _validation;

        public PublishJobPostingService(AddIdentityDbContext context, IJobPostingValidation validation)
        {
            _context = context;
            _validation = validation;
        }

        public async Task<BaseResponse<bool>> PublishAsync(Guid jobPostingId, CancellationToken ct)
        {
            var validation = await _validation.ValidateStatusChangeAsync(jobPostingId, ct);

            if (!validation.Success)
                return ResponseFactory.Fail<bool>(validation.Message, validation.Errors);

            var jobPosting = validation.Data!;

            if (jobPosting.Status == JobPostingStatus.Published)
                return ResponseFactory.Fail<bool>("Job posting is already published.");

            if (jobPosting.Status == JobPostingStatus.Closed)
                return ResponseFactory.Fail<bool>("Closed job posting cannot be published.");

            jobPosting.Status = JobPostingStatus.Published;

            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(true, "Job posting published successfully.");
        }
    }
}

