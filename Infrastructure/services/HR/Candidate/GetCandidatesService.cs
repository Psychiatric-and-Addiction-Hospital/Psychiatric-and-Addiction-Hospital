using Application.Common.Extensions;
using Application.Common.Interfaces.HR.Candidate;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Candidate;
using Application.Queries.HR.Candidate;
using Domain.Entites.HR.Recruitment;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.Candidate
{
    public class GetCandidatesService : IGetCandidates
    {
        private readonly AddIdentityDbContext _context;
        public GetCandidatesService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<PagedResponse<CandidateResponse>>> GetAllAsync(GetCandidatesQuery request, CancellationToken ct)
        {
            var query = _context.Candidates.
                AsNoTracking().
                AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Request.Search))
            {
                var search = request.Request.Search.Trim();

                query = query.Where(x =>

                    x.FirstName.Contains(search) ||

                    x.LastName.Contains(search) ||

                    x.Email.Contains(search) ||

                    x.PhoneNumber.Contains(search));
            }

            if (request.Request.IsActive.HasValue)
            {
                query = query.Where(x =>
                    x.IsActive == request.Request.IsActive);
            }

            query = (request.Request.SortBy?.ToLower()) switch
            {
                "firstname" => request.Request.Descending
                    ? query.OrderByDescending(x => x.FirstName)
                    : query.OrderBy(x => x.FirstName),

                "lastname" => request.Request.Descending
                    ? query.OrderByDescending(x => x.LastName)
                    : query.OrderBy(x => x.LastName),

                "email" => request.Request.Descending
                    ? query.OrderByDescending(x => x.Email)
                    : query.OrderBy(x => x.Email),

                "experience" => request.Request.Descending
                    ? query.OrderByDescending(x => x.YearsOfExperience)
                    : query.OrderBy(x => x.YearsOfExperience),

                _ => request.Request.Descending
                    ? query.OrderByDescending(x => x.FirstName)
                    : query.OrderBy(x => x.FirstName)
            };

            var pagedResult = await query.ToPagedResponseAsync(request.Request.PageNumber, request.Request.PageSize, ct);

            var response = new PagedResponse<CandidateResponse>
            {
                Items = pagedResult.Items.Select(candidate => new CandidateResponse
                {
                    Id = candidate.Id,

                    FirstName = candidate.FirstName,

                    LastName = candidate.LastName,

                    FullName = candidate.FullName,

                    Email = candidate.Email,

                    PhoneNumber = candidate.PhoneNumber,

                    DateOfBirth = candidate.DateOfBirth,

                    YearsOfExperience = candidate.YearsOfExperience,

                    CurrentCompany = candidate.CurrentCompany,

                    CurrentPosition = candidate.CurrentPosition,

                    CurrentSalary = candidate.CurrentSalary,

                    ExpectedSalary = candidate.ExpectedSalary,

                    LinkedInUrl = candidate.LinkedInUrl,

                    ResumeUrl = candidate.ResumeUrl,

                    ImageUrl = candidate.Image,

                    Notes = candidate.Notes,

                    IsActive = candidate.IsActive
                }).ToList(),

                PageNumber = pagedResult.PageNumber,

                PageSize = pagedResult.PageSize,

                TotalPages = pagedResult.TotalPages,

                TotalRecords = pagedResult.TotalRecords
            };

            return ResponseFactory.Success(response, "Candidates retrieved successfully.");
        }
    }
}
