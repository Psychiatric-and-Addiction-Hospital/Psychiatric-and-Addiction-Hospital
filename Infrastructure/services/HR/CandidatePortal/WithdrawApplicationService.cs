using Application.Common.Interfaces.Common;
using Application.Common.Interfaces.HR.Application;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Application;
using Application.DTOS.Responses.HR.ApplicationOffer;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;


namespace Infrastructure.services.HR.CandidatePortal
{
    public class WithdrawApplicationService : IWithdrawApplication
    {
        private readonly AddIdentityDbContext _context;
        private readonly IApplicationValidation _validation;
        private readonly IApplicationStatusService _statusService;

        public WithdrawApplicationService(AddIdentityDbContext context, IApplicationValidation validation, IApplicationStatusService statusService)
        {
            _context = context;
            _validation = validation;
            _statusService = statusService;
        }

        public async Task<BaseResponse<ApplicationResponse>> WithdrawAsync(Guid applicationId, CancellationToken ct)
        {
            var validation = await _validation.ValidateStatusTransitionAsync(applicationId, ApplicationStatus.Withdrawn, ct);

            if (!validation.Success)
                return ResponseFactory.Fail<ApplicationResponse>(validation.Message, validation.Errors);

            var application = validation.Data!;

            var statusResult = await _statusService.ChangeStatusAsync(
                application.Id,
                ApplicationStatus.OfferDeclined,
                "Candidate withdrew the application.", ct);

            if (!statusResult.Success)
                return ResponseFactory.Fail<ApplicationResponse>(statusResult.Message, statusResult.Errors);

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
