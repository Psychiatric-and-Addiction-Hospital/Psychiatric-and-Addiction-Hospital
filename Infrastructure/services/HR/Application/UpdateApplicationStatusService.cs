using Application.Commands.HR.Application;
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

        public UpdateApplicationStatusService(
            AddIdentityDbContext context,
            IApplicationValidation validation)
        {
            _context = context;
            _validation = validation;
        }

        public async Task<BaseResponse<ApplicationResponse>> UpdateAsync(
            UpdateApplicationStatusCommand request,
            CancellationToken ct)
        {
            //----------------------------------------
            // Validation
            //----------------------------------------

            var validation = await _validation.ValidateStatusTransitionAsync(request.Request.Id, request.Request.Status, ct);

            if (!validation.Success)
                return ResponseFactory.Fail<ApplicationResponse>(validation.Message, validation.Errors);


            var application = validation.Data!;

            application.Status = request.Request.Status;

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

            return ResponseFactory.Success(
                response,
                "Application status updated successfully.");
        }
    }

}
