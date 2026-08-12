using Application.Common.Interfaces.HR.Dashboard;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Dashboard;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.Dashboard
{
    public class GetEmployeesByEmploymentStatusService : IGetEmployeesByEmploymentStatus
    {
        private readonly AddIdentityDbContext _context;
        public GetEmployeesByEmploymentStatusService(AddIdentityDbContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse<List<EmployeesByEmploymentStatusResponse>>> GetAsync(CancellationToken ct)
        {
            var result = await _context.Employees
                    .AsNoTracking()
                    .GroupBy(x => x.EmploymentStatus)
                    .Select(g => new EmployeesByEmploymentStatusResponse
                    {
                        EmploymentStatus = g.Key.ToString(),
                        EmployeeCount = g.Count()
                    })
                    .OrderByDescending(x => x.EmployeeCount)
                    .ToListAsync(ct);

            return ResponseFactory.Success(result, "Employees by employment status retrieved successfully.");
        }
    }
}
