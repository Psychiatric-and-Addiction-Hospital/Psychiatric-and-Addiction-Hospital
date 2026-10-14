using Application.Commands.HR.Application;
using Application.Common.Interfaces.HR.Application;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Application;
using Domain.Entites.HR.Recruitment;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.Application
{
    public class CreateApplicationService : ICreateApplication
    {
        private readonly AddIdentityDbContext _context;
        private readonly IApplicationValidation _validation;

        public CreateApplicationService(
            AddIdentityDbContext context,
            IApplicationValidation validation)
        {
            _context = context;
            _validation = validation;
        }

        public async Task<BaseResponse<ApplicationResponse>> CreateAsync(
            CreateApplicationCommand request,
            CancellationToken ct)
        {

            var validation = await _validation
                .ValidateApplyAsync(request.Request, ct);

            if (!validation.Success)
            {
                return ResponseFactory.Fail<ApplicationResponse>(
                    validation.Message,
                    validation.Errors);
            }

            var candidate = await _context.Candidates
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Request.CandidateId, ct);

            if (candidate == null) return ResponseFactory.Fail<ApplicationResponse>("Candidate not found.");

            var application = new Domain.Entites.HR.Recruitment.Application
            {
                CandidateId = request.Request.CandidateId,

                JobPostingId = request.Request.JobPostingId,

                AppliedDate = DateTime.UtcNow,

                Status = ApplicationStatus.Pending,

                Notes = request.Request.Notes?.Trim(),

                CoverLetter = request.Request.CoverLetter?.Trim(),
                ResumeSnapshotUrl = candidate.ResumeUrl
            };

            _context.Applications.Add(application);

            await _context.SaveChangesAsync(ct);

            var createdApplication = await _context.Applications
                .AsNoTracking()
                .Include(x => x.Candidate)
                .Include(x => x.JobPosting)
                    .ThenInclude(j => j.Department)
                .Include(x => x.JobPosting)
                    .ThenInclude(j => j.Position)
                .FirstAsync(x => x.Id == application.Id, ct);

            var response = new ApplicationResponse
            {
                Id = createdApplication.Id,

                CandidateId = createdApplication.CandidateId,

                CandidateName = createdApplication.Candidate.FullName,

                JobPostingId = createdApplication.JobPostingId,

                JobTitle = createdApplication.JobPosting.Title,

                DepartmentName = createdApplication.JobPosting.Department.Name,

                PositionName = createdApplication.JobPosting.Position.Name,

                AppliedDate = createdApplication.AppliedDate,

                Status = createdApplication.Status,

                Notes = createdApplication.Notes,

                CoverLetter = createdApplication.CoverLetter,

                ResumeSnapshotUrl = createdApplication.ResumeSnapshotUrl
            };

            return ResponseFactory.Success(response, "Application submitted successfully.");
        }
    }
}

