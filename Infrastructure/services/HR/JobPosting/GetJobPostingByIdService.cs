using Application.Common.Interfaces.HR.JobPosting;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.JobPosting;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.JobPosting
{
    public class GetJobPostingByIdService : IGetJobPostingById
    {
        private readonly AddIdentityDbContext _context;

        public GetJobPostingByIdService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<JobPostingResponse>> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var jobPosting = await _context.JobPostings
                .AsNoTracking()
                .Include(x => x.Department)
                .Include(x => x.Position)
                .Include(x => x.HiringManager)
                .FirstOrDefaultAsync(x => x.Id == id, ct);

            if (jobPosting == null)
                return ResponseFactory.Fail<JobPostingResponse>("Job posting not found.");

            var response = new JobPostingResponse
            {
                Id = jobPosting.Id,

                Title = jobPosting.Title,

                Description = jobPosting.Description,

                Location = jobPosting.Location,

                MinSalary = jobPosting.MinSalary,

                MaxSalary = jobPosting.MaxSalary,

                Vacancies = jobPosting.Vacancies,

                WorkMode = jobPosting.WorkMode,

                EmploymentType = jobPosting.EmploymentType,

                ExperienceLevel = jobPosting.ExperienceLevel,

                PublishedDate = jobPosting.PublishedDate,

                ClosingDate = jobPosting.ClosingDate,

                Status = jobPosting.Status,

                DepartmentId = jobPosting.DepartmentId,

                DepartmentName = jobPosting.Department.Name,

                PositionId = jobPosting.PositionId,

                PositionName = jobPosting.Position.Name,

                HiringManagerId = jobPosting.HiringManagerId,

                HiringManagerName = jobPosting.HiringManager.FullName
            };

            return ResponseFactory.Success(response, "Job posting retrieved successfully.");
        }
    }
}
