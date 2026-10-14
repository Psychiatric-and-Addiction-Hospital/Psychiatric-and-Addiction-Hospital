using Application.Common.Extensions;
using Application.Common.Interfaces.HR.ApplicationOffer;
using Application.Common.Responses;
using Application.DTOS.Request.HR.ApplicationOffer;
using Application.DTOS.Responses.HR.ApplicationOffer;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.ApplicationOffer
{
    public class GetApplicationOffersService : IGetApplicationOffers
    {
        private readonly AddIdentityDbContext _context;

        public GetApplicationOffersService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<PagedResponse<ApplicationOfferResponse>>> GetAllAsync(ApplicationOfferListRequest request, CancellationToken ct)
        {
            var query = _context.ApplicationOffers
                .AsNoTracking()
                .Include(x => x.Application)
                    .ThenInclude(x => x.Candidate)
                .Include(x => x.Application)
                    .ThenInclude(x => x.JobPosting)
                        .ThenInclude(x => x.Department)
                .Include(x => x.Application)
                    .ThenInclude(x => x.JobPosting)
                        .ThenInclude(x => x.Position)
                .Include(x => x.ApprovedByEmployee)
                .Include(x => x.Contract)
                .AsQueryable();

            //search

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();

                query = query.Where(x =>

                    x.Application.Candidate.FullName.Contains(search)

                    ||

                    x.Application.JobPosting.Title.Contains(search)

                    ||

                    x.Application.JobPosting.Department.Name.Contains(search)

                    ||

                    x.Application.JobPosting.Position.Name.Contains(search));
            }

            //Filters
            if (request.Status.HasValue)
            {
                query = query.Where(x =>
                    x.Status == request.Status.Value);
            }

            if (request.DepartmentId.HasValue)
            {
                query = query.Where(x =>
                    x.Application.JobPosting.DepartmentId ==
                    request.DepartmentId.Value);
            }

            if (request.PositionId.HasValue)
            {
                query = query.Where(x =>
                    x.Application.JobPosting.PositionId ==
                    request.PositionId.Value);
            }

            if (request.ApprovedByEmployeeId.HasValue)
            {
                query = query.Where(x =>
                    x.ApprovedByEmployeeId ==
                    request.ApprovedByEmployeeId.Value);
            }

            if (request.MinSalary.HasValue)
            {
                query = query.Where(x =>
                    x.OfferedSalary >= request.MinSalary.Value);
            }

            if (request.MaxSalary.HasValue)
            {
                query = query.Where(x =>
                    x.OfferedSalary <= request.MaxSalary.Value);
            }

            if (request.FromOfferDate.HasValue)
            {
                query = query.Where(x =>
                    x.OfferDate >= request.FromOfferDate.Value);
            }

            if (request.ToOfferDate.HasValue)
            {
                query = query.Where(x =>
                    x.OfferDate <= request.ToOfferDate.Value);
            }

            //Sorting
            query = (request.SortBy?.ToLower()) switch
            {
                "candidate" => request.Descending
                    ? query.OrderByDescending(x => x.Application.Candidate.FullName)
                    : query.OrderBy(x => x.Application.Candidate.FullName),

                "salary" => request.Descending
                    ? query.OrderByDescending(x => x.OfferedSalary)
                    : query.OrderBy(x => x.OfferedSalary),

                "jobtitle" => request.Descending
                    ? query.OrderByDescending(x => x.Application.JobPosting.Title)
                    : query.OrderBy(x => x.Application.JobPosting.Title),

                "offerdate" => request.Descending
                    ? query.OrderByDescending(x => x.OfferDate)
                    : query.OrderBy(x => x.OfferDate),

                _ => query.OrderByDescending(x => x.OfferDate)
            };

            var responseQuery = query.Select(x => new ApplicationOfferResponse
            {
                Id = x.Id,

                ApplicationId = x.ApplicationId,

                CandidateId = x.Application.CandidateId,

                CandidateName = x.Application.Candidate.FullName,

                JobPostingId = x.Application.JobPostingId,

                JobTitle = x.Application.JobPosting.Title,

                DepartmentName =
            x.Application.JobPosting.Department.Name,

                PositionName =
            x.Application.JobPosting.Position.Name,

                OfferedSalary = x.OfferedSalary,

                OfferDate = x.OfferDate,

                ExpiryDate = x.ExpiryDate,

                ResponseDate = x.ResponseDate,

                Status = x.Status,

                Notes = x.Notes,

                ApprovedByEmployeeId =
            x.ApprovedByEmployeeId,

                ApprovedByEmployeeName =
            x.ApprovedByEmployee != null
                ? x.ApprovedByEmployee.FullName
                : null,

                HasContract = x.Contract != null
            });

            var paged = await responseQuery.ToPagedResponseAsync(request.PageNumber, request.PageSize, ct);

            return ResponseFactory.Success(paged, "Application offers retrieved successfully.");
        }
    }
}