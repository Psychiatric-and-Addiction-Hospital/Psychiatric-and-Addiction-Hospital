using Application.Common.Extensions;
using Application.Common.Interfaces.HR.JobPosting;
using Application.Common.Responses;
using Application.DTOS.Request.HR.JobPosting;
using Application.DTOS.Responses.HR.JobPosting;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.JobPosting
{
    public class GetJobPostingsService : IGetJobPostings
    {
        private readonly AddIdentityDbContext _context;
        public GetJobPostingsService(AddIdentityDbContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse<PagedResponse<JobPostingResponse>>> GetAllAsync(JobPostingListRequest request, CancellationToken ct)
        {
            var query = _context.JobPostings
                .AsNoTracking()
                .Include(x => x.Department)
                .Include(x => x.Position)
                .Include(x => x.HiringManager)
                .AsQueryable();


            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();

                query = query.Where(x =>
                    x.Title.Contains(search));
            }

            if (request.DepartmentId.HasValue)
                query = query.Where(x => x.DepartmentId == request.DepartmentId);

            if (request.Status.HasValue)
                query = query.Where(x => x.Status == request.Status);

            if (request.WorkMode.HasValue)
                query = query.Where(x => x.WorkMode == request.WorkMode);

            if (request.EmploymentType.HasValue)
                query = query.Where(x => x.EmploymentType == request.EmploymentType);

            query = request.Descending
                ? query.OrderByDescending(x => x.PublishedDate)
                : query.OrderBy(x => x.PublishedDate);

            var responseQuery = query.Select(x => new JobPostingResponse
            {
                Id = x.Id,

                Title = x.Title,

                Description = x.Description,

                Location = x.Location,

                MinSalary = x.MinSalary,

                MaxSalary = x.MaxSalary,

                Vacancies = x.Vacancies,

                WorkMode = x.WorkMode,

                EmploymentType = x.EmploymentType,

                ExperienceLevel = x.ExperienceLevel,

                PublishedDate = x.PublishedDate,

                ClosingDate = x.ClosingDate,

                Status = x.Status,

                DepartmentId = x.DepartmentId,

                DepartmentName = x.Department.Name,

                PositionId = x.PositionId,

                PositionName = x.Position.Name,
            });


            var pagedResult = await responseQuery.ToPagedResponseAsync(request.PageNumber, request.PageSize, ct);

            return ResponseFactory.Success(pagedResult, "Job postings retrieved successfully.");
        }
    }

}

