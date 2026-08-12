using Application.Common.Interfaces.HR.Dashboard;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Dashboard;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.Dashboard
{
    public class GetEmployeesByDepartmentService : IGetEmployeesByDepartment
    {
        private readonly AddIdentityDbContext _context;
        public GetEmployeesByDepartmentService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<List<EmployeesByDepartmentResponse>>> GetAsync(CancellationToken ct)
        {
            var result = await _context.Employees
                .AsNoTracking()
                .GroupBy(e => e.Department.Name)
                .Select(g => new EmployeesByDepartmentResponse
                {
                    DepartmentName = g.Key,
                    EmployeeCount = g.Count()
                }).OrderByDescending(x => x.EmployeeCount)
                .ToListAsync(ct);

            return ResponseFactory.Success(result,"Employees by department retrieved successfully.");
        }
    }
}
