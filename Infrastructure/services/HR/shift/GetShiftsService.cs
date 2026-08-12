using Application.Common.Extensions;
using Application.Common.Interfaces.HR.Shift;
using Application.Common.Responses;
using Application.DTOS.Request.HR.Shift;
using Application.DTOS.Responses.HR.Shift;
using Azure.Core;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.shift
{
    public class GetShiftsService : IGetShifts
    {
        private readonly AddIdentityDbContext _context;

        public GetShiftsService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<PagedResponse<ShiftResponse>>> GetAllAsync(ShiftListRequest request, CancellationToken ct)
        {
            var query = _context.Shifts.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();

                query = query.Where(x =>
                    x.Name.Contains(search));
            }

            if (request.IsActive.HasValue)
            {
                query = query.Where(x =>
                    x.IsActive == request.IsActive.Value);
            }

            if (request.IsNightShift.HasValue)
            {
                query = query.Where(x =>
                    x.IsNightShift == request.IsNightShift.Value);
            }

            query = request.SortBy?.ToLower() switch
            {
                "name" => request.Descending
                    ? query.OrderByDescending(x => x.Name)
                    : query.OrderBy(x => x.Name),

                "starttime" => request.Descending
                    ? query.OrderByDescending(x => x.StartTime)
                    : query.OrderBy(x => x.StartTime),

                _ => query.OrderBy(x => x.Name)
            };

            var response = query.Select(x => new ShiftResponse
            {
                Id = x.Id,

                Name = x.Name,

                StartTime = x.StartTime,

                EndTime = x.EndTime,

                BreakMinutes = x.BreakMinutes,

                IsNightShift = x.IsNightShift,

                ToleranceMinutes = x.ToleranceMinutes,

                IsActive = x.IsActive
            });

            var pagedResult = await response.ToPagedResponseAsync(request.PageNumber, request.PageSize, ct);

            return ResponseFactory.Success(pagedResult, "Shifts retrieved successfully.");
        }
    }
}

