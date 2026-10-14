using Application.Common.Extensions;
using Application.Common.Interfaces.HR.Employee;
using Application.Common.Responses;
using Application.DTOS.Request.HR.Employee;
using Application.DTOS.Responses.HR.Employee;
using Infrastructure.Persistence.Extensions;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.Employee
{
    public class GetEmployeesService : IGetEmployees
    {
        private readonly AddIdentityDbContext _context;
        public GetEmployeesService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<PagedResponse<EmployeeResponse>>> GetAllAsync(EmployeeListRequest request, CancellationToken ct)
        {
            var query = _context.WithRole();
            #region Search

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();

                query = query.Where(x =>
                    x.Employee.EmployeeCode.Contains(search) ||
                    x.Employee.FirstName.Contains(search) ||
                    x.Employee.LastName.Contains(search) ||
                    (x.Employee.FullName).Contains(search) ||
                    x.Employee.Email.Contains(search));
            }

            #endregion

            #region Filters

            if (request.DepartmentId.HasValue)
                query = query.Where(x => x.Employee.DepartmentId == request.DepartmentId);


            if (request.PositionId.HasValue)
                query = query.Where(x => x.Employee.PositionId == request.PositionId);


            if (request.ShiftId.HasValue)
                query = query.Where(x => x.Employee.ShiftId == request.ShiftId);


            if (!string.IsNullOrWhiteSpace(request.Role))
                query = query.Where(x => x.Role == request.Role);


            if (request.EmploymentStatus.HasValue)
                query = query.Where(x => x.Employee.EmploymentStatus == request.EmploymentStatus);


            if (request.IsActive.HasValue)
                query = query.Where(x => x.Employee.IsActive == request.IsActive);


            #endregion

            #region Sorting

            query = request.SortBy?.ToLower() switch
            {
                "name" => request.Descending
                    ? query.OrderByDescending(x => x.Employee.FirstName)
                    : query.OrderBy(x => x.Employee.FirstName),

                "hiredate" => request.Descending
                    ? query.OrderByDescending(x => x.Employee.HireDate)
                    : query.OrderBy(x => x.Employee.HireDate),

                "department" => request.Descending
                    ? query.OrderByDescending(x => x.Employee.Department.Name)
                    : query.OrderBy(x => x.Employee.Department.Name),

                "role" => request.Descending
                ? query.OrderByDescending(x => x.Role)
                : query.OrderBy(x => x.Role),

                _ => request.Descending
                    ? query.OrderByDescending(x => x.Employee.EmployeeCode)
                    : query.OrderBy(x => x.Employee.EmployeeCode)
            };

            #endregion
            var responseQuery = query.Select(x => new EmployeeResponse
            {
                Id = x.Employee.Id,
                EmployeeCode = x.Employee.EmployeeCode,
                FullName = x.Employee.FullName,
                Email = x.Employee.Email,
                PhoneNumber = x.Employee.PhoneNumber,
                DepartmentName = x.Employee.Department.Name,
                PositionName = x.Employee.Position.Name,
                ShiftName = x.Employee.Shift.Name,
                EmploymentStatus = x.Employee.EmploymentStatus,
                IsActive = x.Employee.IsActive,
                HireDate = x.Employee.HireDate,
                ImageUrl = x.Employee.AppUser.ImageUrl,
                Role = x.Role
            });

            var paged = await responseQuery.ToPagedResponseAsync(request.PageNumber, request.PageSize, ct);

            return ResponseFactory.Success(paged);
        }
    }
}
