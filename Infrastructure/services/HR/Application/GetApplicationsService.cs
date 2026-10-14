using Application.Common.Extensions;
using Application.Common.Interfaces.HR.Application;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Application;
using Application.Queries.HR.Application;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.Application
{
    public class GetApplicationsService : IGetApplications
    {
        private readonly AddIdentityDbContext _context;

        public GetApplicationsService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<PagedResponse<ApplicationResponse>>> GetAllAsync(
            GetApplicationsQuery request,
            CancellationToken ct)
        {
            //----------------------------------------
            // Query
            //----------------------------------------

            var query = _context.Applications
                .AsNoTracking()
                .AsQueryable();

            //----------------------------------------
            // Search
            //----------------------------------------

            if (!string.IsNullOrWhiteSpace(request.Request.Search))
            {
                var search = request.Request.Search.Trim();

                query = query.Where(x =>
                    x.Candidate.FullName.Contains(search) ||
                    x.JobPosting.Title.Contains(search));
            }

            //----------------------------------------
            // Filters
            //----------------------------------------

            if (request.Request.Status.HasValue)
            {
                query = query.Where(x =>
                    x.Status == request.Request.Status.Value);
            }

            if (request.Request.CandidateId.HasValue)
            {
                query = query.Where(x =>
                    x.CandidateId == request.Request.CandidateId.Value);
            }

            if (request.Request.JobPostingId.HasValue)
            {
                query = query.Where(x =>
                    x.JobPostingId == request.Request.JobPostingId.Value);
            }

            if (request.Request.FromDate.HasValue)
            {
                query = query.Where(x =>
                    x.AppliedDate >= request.Request.FromDate.Value);
            }

            if (request.Request.ToDate.HasValue)
            {
                query = query.Where(x =>
                    x.AppliedDate <= request.Request.ToDate.Value);
            }

            //----------------------------------------
            // Sorting
            //----------------------------------------

            query = request.Request.SortBy?.ToLower() switch
            {
                "candidate" => request.Request.Descending
                    ? query.OrderByDescending(x => x.Candidate.FullName)
                    : query.OrderBy(x => x.Candidate.FullName),

                "jobtitle" => request.Request.Descending
                    ? query.OrderByDescending(x => x.JobPosting.Title)
                    : query.OrderBy(x => x.JobPosting.Title),

                "status" => request.Request.Descending
                    ? query.OrderByDescending(x => x.Status)
                    : query.OrderBy(x => x.Status),

                _ => request.Request.Descending
                    ? query.OrderByDescending(x => x.AppliedDate)
                    : query.OrderBy(x => x.AppliedDate)
            };

            //----------------------------------------
            // Projection
            //----------------------------------------

            var result = query.Select(x => new ApplicationResponse
            {
                Id = x.Id,

                CandidateId = x.CandidateId,

                CandidateName = x.Candidate.FullName,

                JobPostingId = x.JobPostingId,

                JobTitle = x.JobPosting.Title,

                DepartmentName = x.JobPosting.Department.Name,

                PositionName = x.JobPosting.Position.Name,

                AppliedDate = x.AppliedDate,

                Status = x.Status,

                Notes = x.Notes,

                CoverLetter = x.CoverLetter,

                ResumeSnapshotUrl = x.ResumeSnapshotUrl,

                InterviewsCount = x.Interviews.Count,

                HasOffer = x.Offer != null
            });

            //----------------------------------------
            // Pagination
            //----------------------------------------

            var pagedResult = await result.ToPagedResponseAsync(
                request.Request.PageNumber,
                request.Request.PageSize,
                ct);

            //----------------------------------------
            // Response
            //----------------------------------------

            return ResponseFactory.Success(pagedResult, "Applications retrieved successfully.");
        }
    }
}