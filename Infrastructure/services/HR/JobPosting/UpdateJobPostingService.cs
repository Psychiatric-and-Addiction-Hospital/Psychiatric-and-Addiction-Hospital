using Application.Commands.HR.JobPosting;
using Application.Common.Interfaces.HR.JobPosting;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.JobPosting;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.JobPosting
{
    public class UpdateJobPostingService : IUpdateJobPosting
    {
        private readonly AddIdentityDbContext _context;
        private readonly IJobPostingValidation _validation;

        public UpdateJobPostingService(
            AddIdentityDbContext context,
            IJobPostingValidation validation)
        {
            _context = context;
            _validation = validation;
        }

        public async Task<BaseResponse<JobPostingResponse>> UpdateAsync(
            UpdateJobPostingCommand request,
            CancellationToken ct)
        {

            var validation = await _validation.ValidateUpdateAsync(request.Request, ct);

            if (!validation.Success)
                return ResponseFactory.Fail<JobPostingResponse>(validation.Message, validation.Errors);


            var jobPosting = validation.Data!;

            jobPosting.Title = request.Request.Title.Trim();

            jobPosting.Description = request.Request.Description.Trim();

            jobPosting.Location = request.Request.Location.Trim();

            jobPosting.MinSalary = request.Request.MinSalary;

            jobPosting.MaxSalary = request.Request.MaxSalary;

            jobPosting.Vacancies = request.Request.Vacancies;

            jobPosting.WorkMode = request.Request.WorkMode;

            jobPosting.EmploymentType = request.Request.EmploymentType;

            jobPosting.ExperienceLevel = request.Request.ExperienceLevel;

            jobPosting.PublishedDate = request.Request.PublishedDate;

            jobPosting.ClosingDate = request.Request.ClosingDate;

            jobPosting.DepartmentId = request.Request.DepartmentId;

            jobPosting.PositionId = request.Request.PositionId;

            await _context.SaveChangesAsync(ct);

            var updatedJobPosting = await _context.JobPostings
                .AsNoTracking()
                .Include(x => x.Department)
                .Include(x => x.Position)
                .Include(x => x.HiringManager)
                .FirstAsync(x => x.Id == jobPosting.Id, ct);

            var response = new JobPostingResponse
            {
                Id = updatedJobPosting.Id,

                Title = updatedJobPosting.Title,

                Description = updatedJobPosting.Description,

                Location = updatedJobPosting.Location,

                MinSalary = updatedJobPosting.MinSalary,

                MaxSalary = updatedJobPosting.MaxSalary,

                Vacancies = updatedJobPosting.Vacancies,

                WorkMode = updatedJobPosting.WorkMode,

                EmploymentType = updatedJobPosting.EmploymentType,

                ExperienceLevel = updatedJobPosting.ExperienceLevel,

                PublishedDate = updatedJobPosting.PublishedDate,

                ClosingDate = updatedJobPosting.ClosingDate,

                Status = updatedJobPosting.Status,

                DepartmentId = updatedJobPosting.DepartmentId,

                DepartmentName = updatedJobPosting.Department.Name,

                PositionId = updatedJobPosting.PositionId,

                PositionName = updatedJobPosting.Position.Name,
            };

            return ResponseFactory.Success(response, "Job posting updated successfully.");
        }
    }
}

