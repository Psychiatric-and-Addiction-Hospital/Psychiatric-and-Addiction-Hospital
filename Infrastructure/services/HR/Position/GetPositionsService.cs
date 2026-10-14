using Application.Common.Extensions;
using Application.Common.Interfaces.HR.Position;
using Application.Common.Responses;
using Application.DTOS.Request.HR.Position;
using Application.DTOS.Responses.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.Position
{
    public class GetPositionsService : IGetPositions
    {
        private readonly AddIdentityDbContext _context;

        public GetPositionsService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<PagedResponse<PositionResponse>>> GetAllPositionsAsync(PositionListRequest request, CancellationToken ct)
        {
            var query = _context.Positions
             .AsNoTracking()
             .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();

                query = query.Where(x =>
                    x.Name.Contains(search) ||
                    (x.Description != null &&
                     x.Description.Contains(search)));
            }

            if (request.DepartmentId.HasValue)
            {
                query = query.Where(x =>
                    x.DepartmentId == request.DepartmentId);
            }

            if (request.IsActive.HasValue)
            {
                query = query.Where(x =>
                    x.IsActive == request.IsActive);
            }

            query = request.SortBy?.ToLower() switch
            {
                "salary" => request.Descending
                    ? query.OrderByDescending(x => x.BasicSalary)
                    : query.OrderBy(x => x.BasicSalary),

                "department" => request.Descending
                    ? query.OrderByDescending(x => x.Department.Name)
                    : query.OrderBy(x => x.Department.Name),

                "active" => request.Descending
                    ? query.OrderByDescending(x => x.IsActive)
                    : query.OrderBy(x => x.IsActive),

                _ => request.Descending
                    ? query.OrderByDescending(x => x.Name)
                    : query.OrderBy(x => x.Name)
            };

            var responseQuery = query.Select(x => new PositionResponse
            {
                Id = x.Id,

                Name = x.Name,

                Description = x.Description,

                BasicSalary = x.BasicSalary,

                IsActive = x.IsActive,

                DepartmentId = x.DepartmentId,

                DepartmentName = x.Department.Name
            });

            var paged = await responseQuery.ToPagedResponseAsync(request.PageNumber, request.PageSize, ct);

            return ResponseFactory.Success(paged, "Positions retrieved successfully.");

        }
    }
}