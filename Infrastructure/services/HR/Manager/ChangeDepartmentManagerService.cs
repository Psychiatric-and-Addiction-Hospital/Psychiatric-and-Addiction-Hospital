using Application.Common.Interfaces.HR.Manager;
using Application.Common.Responses;
using Application.DTOS.Request.HR.manager;
using Application.DTOS.Responses.HR.Manager;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.services.HR.Manager
{
    public class ChangeDepartmentManagerService : IChangeDepartmentManager
    {
        private readonly AddIdentityDbContext _context;
        public ChangeDepartmentManagerService(AddIdentityDbContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse<DepartmentManagerResponse>> ChangeAsync(ChangeDepartmentManagerRequest request, CancellationToken ct)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(x => x.Id == request.DepartmentId, ct);

            if (department == null)
                return ResponseFactory.Fail<DepartmentManagerResponse>("Department not found.");

            if (department.ManagerId == null)
                return ResponseFactory.Fail<DepartmentManagerResponse>("Department has no manager.");

            var employee = await _context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.NewManagerId, ct);

            if (employee == null)
                return ResponseFactory.Fail<DepartmentManagerResponse>("Employee not found.");

            if (!employee.IsActive)
                return ResponseFactory.Fail<DepartmentManagerResponse>("Employee is not active.");

            if (employee.DepartmentId != department.Id)
                return ResponseFactory.Fail<DepartmentManagerResponse>("Employee does not belong to this department.");

            if (department.ManagerId == employee.Id)
                return ResponseFactory.Fail<DepartmentManagerResponse>("This employee is already the manager.");

            department.ManagerId = employee.Id;
            await _context.SaveChangesAsync(ct);

            var response = new DepartmentManagerResponse
            {
                DepartmentId = department.Id,
                DepartmentName = department.Name,
                ManagerId = employee.Id,
                ManagerName = employee.FullName
            };

            return ResponseFactory.Success(response, "Department manager changed successfully.");
        }
    }
}
