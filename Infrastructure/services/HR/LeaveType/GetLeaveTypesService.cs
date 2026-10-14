using Application.Common.Extensions;
using Application.Common.Interfaces.HR.LeaveType;
using Application.Common.Responses;
using Application.DTOS.Request.HR.LeaveType;
using Application.DTOS.Responses.HR.LeaveType;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.LeaveType
{
    public class GetLeaveTypesService : IGetLeaveTypes
    {
        private readonly AddIdentityDbContext _context;
        public GetLeaveTypesService(AddIdentityDbContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse<PagedResponse<LeaveTypeResponse>>> GetAllAsync(LeaveTypeListRequest request, CancellationToken ct)
        {
            var query = _context.LeaveTypes.AsNoTracking()
                     .Include(x => x.LeaveRequests)
                     .Include(x => x.EmployeeLeaveBalances)
                     .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();

                query = query.Where(x => x.Name.Contains(search)
                || (x.Description != null
                && x.Description.Contains(search)));
            }

            if (request.IsActive.HasValue)
                query = query.Where(x =>
                    x.IsActive == request.IsActive);

            if (request.IsPaid.HasValue)
                query = query.Where(x =>
                    x.IsPaid == request.IsPaid);

            if (request.RequiresApproval.HasValue)
                query = query.Where(x =>
                    x.RequiresApproval ==
                    request.RequiresApproval);

            query = request.SortBy?.ToLower() switch
            {
                "name" => request.Descending
                    ? query.OrderByDescending(x => x.Name)
                    : query.OrderBy(x => x.Name),

                "days" => request.Descending
                    ? query.OrderByDescending(x => x.MaxDaysPerYear)
                    : query.OrderBy(x => x.MaxDaysPerYear),

                _ => request.Descending
                    ? query.OrderByDescending(x => x.Name)
                    : query.OrderBy(x => x.Name)
            };

            var responseQuery = query.Select(x => new LeaveTypeResponse
            {
                Id = x.Id,

                Name = x.Name,

                Description = x.Description,

                MaxDaysPerYear = x.MaxDaysPerYear,

                IsPaid = x.IsPaid,

                RequiresApproval = x.RequiresApproval,

                AllowHalfDay = x.AllowHalfDay,

                IsActive = x.IsActive
            });


            var paged = await responseQuery.ToPagedResponseAsync(
                request.PageNumber, request.PageSize, ct);

            return ResponseFactory.Success(paged, "Leave types retrieved successfully.");
        }
    }
}
