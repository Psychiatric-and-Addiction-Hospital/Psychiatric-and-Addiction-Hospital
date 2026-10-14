using Application.Common.Interfaces.HR.Manager;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Manager;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.Manager
{
    public class RemoveDepartmentManagerService : IRemoveDepartmentManager
    {
        private readonly AddIdentityDbContext _context;
        public RemoveDepartmentManagerService(AddIdentityDbContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse<DepartmentManagerResponse>> RemoveAsync(Guid departmentId, CancellationToken ct)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == departmentId, ct);

            if (department == null)
                return ResponseFactory.Fail<DepartmentManagerResponse>($"Department with ID {departmentId} not found.");

            if (department.ManagerId == null)
                return ResponseFactory.Fail<DepartmentManagerResponse>("This department has no manager.");

            department.ManagerId = null;

            await _context.SaveChangesAsync(ct);
            return ResponseFactory.Success(new DepartmentManagerResponse
            {
                DepartmentId = department.Id,
                DepartmentName = department.Name,
                ManagerId = null,
                ManagerName = null
            }, "Department manager removed successfully.");
        }
    }
}
