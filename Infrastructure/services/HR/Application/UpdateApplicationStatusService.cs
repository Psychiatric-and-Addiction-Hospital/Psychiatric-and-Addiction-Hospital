using Application.Commands.HR.Application;
using Application.Common.Interfaces.Common;
using Application.Common.Interfaces.HR.Application;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Application;
using Infrastructure.Persistence.Identity;


namespace Infrastructure.services.HR.Application
{
    public class UpdateApplicationStatusService : IUpdateApplicationStatus
    {
        private readonly AddIdentityDbContext _context;
        private readonly IApplicationValidation _validation;
        private readonly IApplicationStatusService _statusService;

        public UpdateApplicationStatusService(
            AddIdentityDbContext context,
            IApplicationValidation validation,
            IApplicationStatusService statusService)
        {
            _context = context;
            _validation = validation;
            _statusService = statusService;
        }

        public async Task<BaseResponse<ApplicationResponse>> UpdateAsync(UpdateApplicationStatusCommand request, CancellationToken ct)
        {
            var validation = await _validation.ValidateStatusTransitionAsync(request.Request.Id, request.Request.Status, ct);

            if (!validation.Success)
                return ResponseFactory.Fail<ApplicationResponse>(validation.Message, validation.Errors);


            var application = validation.Data!;

            var statusResult =
                await _statusService.ChangeStatusAsync(application.Id, request.Request.Status, request.Request.Notes?.Trim(), ct);

            if (!statusResult.Success)
                return ResponseFactory.Fail<ApplicationResponse>(statusResult.Message, statusResult.Errors);

            application.Notes = request.Request.Notes?.Trim();

            await _context.SaveChangesAsync(ct);

            var response = new ApplicationResponse
            {
                Id = application.Id,

                CandidateId = application.CandidateId,

                CandidateName = application.Candidate.FullName,

                JobPostingId = application.JobPostingId,

                JobTitle = application.JobPosting.Title,

                DepartmentName = application.JobPosting.Department.Name,

                PositionName = application.JobPosting.Position.Name,

                AppliedDate = application.AppliedDate,

                Status = application.Status,

                Notes = application.Notes,

                CoverLetter = application.CoverLetter,

                ResumeSnapshotUrl = application.ResumeSnapshotUrl
            };

            return ResponseFactory.Success(response, "Application status updated successfully.");
        }
    }

}
