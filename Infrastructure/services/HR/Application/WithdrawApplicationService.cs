using Application.Common.Interfaces.HR.Application;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Application;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;


namespace Infrastructure.services.HR.Application
{
    public class WithdrawApplicationService : IWithdrawApplication
    {
        private readonly AddIdentityDbContext _context;
        private readonly IApplicationValidation _validation;

        public WithdrawApplicationService(AddIdentityDbContext context, IApplicationValidation validation)
        {
            _context = context;
            _validation = validation;
        }

        public async Task<BaseResponse<ApplicationResponse>> WithdrawAsync(Guid applicationId, CancellationToken ct)
        {
            var validation = await _validation.ValidateStatusTransitionAsync(applicationId, ApplicationStatus.Withdrawn, ct);

            if (!validation.Success)
                return ResponseFactory.Fail<ApplicationResponse>(validation.Message, validation.Errors);

            var application = validation.Data!;

            application.Status = ApplicationStatus.Withdrawn;

            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(new ApplicationResponse
            {
                Id = application.Id,
                CandidateId = application.CandidateId,
                JobPostingId = application.JobPostingId,
                AppliedDate = application.AppliedDate,
                Status = application.Status,
                Notes = application.Notes,
                CoverLetter = application.CoverLetter,
                ResumeSnapshotUrl = application.ResumeSnapshotUrl
            }, "Application withdrawn successfully.");
        }
    }
}
