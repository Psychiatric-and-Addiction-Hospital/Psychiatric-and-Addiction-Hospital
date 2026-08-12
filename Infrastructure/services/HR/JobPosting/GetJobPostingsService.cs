using Application.Common.Extensions;
using Application.Common.Interfaces.HR.JobPosting;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.JobPosting;
using Application.Queries.HR.JobPosting;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.JobPosting
{
    public class GetJobPostingsService : IGetJobPostings
    {
        private readonly AddIdentityDbContext _context;
        public GetJobPostingsService(AddIdentityDbContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse<PagedResponse<JobPostingResponse>>> GetAllAsync(GetJobPostingsQuery request, CancellationToken ct)
        {
            var query = _context.JobPostings
                .AsNoTracking()
                .Include(x => x.Department)
                .Include(x => x.Position)
                .Include(x => x.HiringManager)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Request.Search))
            {
                var search = request.Request.Search.Trim();

                query = query.Where(x =>
                    x.Title.Contains(search));
            }

            if (request.Request.DepartmentId.HasValue)
                query = query.Where(x => x.DepartmentId == request.Request.DepartmentId);

            if (request.Request.Status.HasValue)
                query = query.Where(x => x.Status == request.Request.Status);

            if (request.Request.WorkMode.HasValue)
                query = query.Where(x => x.WorkMode == request.Request.WorkMode);

            if (request.Request.EmploymentType.HasValue)
                query = query.Where(x => x.EmploymentType == request.Request.EmploymentType);

            query = request.Request.Descending
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

                HiringManagerId = x.HiringManagerId,

                HiringManagerName = x.HiringManager.FullName
            });


            var pagedResult = await responseQuery.ToPagedResponseAsync(request.Request.PageNumber, request.Request.PageSize, ct);

            return ResponseFactory.Success(pagedResult, "Job postings retrieved successfully.");
        }
    }

}

