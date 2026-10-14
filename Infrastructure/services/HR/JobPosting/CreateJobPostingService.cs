using Application.Commands.HR.JobPosting;
using Application.Common.Interfaces.HR.JobPosting;
using Application.Common.Responses;
using Application.DTOS.Request.HR.JobPosting;
using Application.DTOS.Responses.HR.JobPosting;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.JobPosting
{
    public class CreateJobPostingService : ICreateJobPosting
    {
        private readonly AddIdentityDbContext _context;
        private readonly IJobPostingValidation _validation;
        public CreateJobPostingService(AddIdentityDbContext context, IJobPostingValidation validation)
        {
            _context = context;
            _validation = validation;
        }

        public async Task<BaseResponse<JobPostingResponse>> CreateAsync(
            CreateJobPostingRequest request,
            CancellationToken ct)
        {
            var validation = await _validation.ValidateCreateAsync(request, ct);

            if (!validation.Success)
                return ResponseFactory.Fail<JobPostingResponse>(validation.Message,validation.Errors);

            var jobPosting = new Domain.Entites.HR.Recruitment.JobPosting
            {
                Title = request.Title.Trim(),

                Description = request.Description.Trim(),

                Location = request.Location.Trim(),

                MinSalary = request.MinSalary,

                MaxSalary = request.MaxSalary,

                Vacancies = request.Vacancies,

                WorkMode = request.WorkMode,

                EmploymentType = request.EmploymentType,

                ExperienceLevel = request.ExperienceLevel,

                PublishedDate = request.PublishedDate,

                ClosingDate = request.ClosingDate,

                Status = JobPostingStatus.Draft,

                DepartmentId = request.DepartmentId,

                PositionId = request.PositionId,

            };

            _context.JobPostings.Add(jobPosting);

            await _context.SaveChangesAsync(ct);

            var createdJobPosting = await _context.JobPostings
               .AsNoTracking()
               .Include(x => x.Department)
               .Include(x => x.Position)
               .Include(x => x.HiringManager)
               .FirstAsync(x => x.Id == jobPosting.Id, ct);

            var response = new JobPostingResponse
            {
                Id = createdJobPosting.Id,

                Title = createdJobPosting.Title,

                Description = createdJobPosting.Description,

                Location = createdJobPosting.Location,

                MinSalary = createdJobPosting.MinSalary,

                MaxSalary = createdJobPosting.MaxSalary,

                Vacancies = createdJobPosting.Vacancies,

                WorkMode = createdJobPosting.WorkMode,

                EmploymentType = createdJobPosting.EmploymentType,

                ExperienceLevel = createdJobPosting.ExperienceLevel,

                PublishedDate = createdJobPosting.PublishedDate,

                ClosingDate = createdJobPosting.ClosingDate,

                Status = createdJobPosting.Status,

                DepartmentId = createdJobPosting.DepartmentId,

                DepartmentName = createdJobPosting.Department.Name,

                PositionId = createdJobPosting.PositionId,

                PositionName = createdJobPosting.Position.Name,

            };

            return ResponseFactory.Success(response, "Job posting created successfully.");
        }

    }
}
