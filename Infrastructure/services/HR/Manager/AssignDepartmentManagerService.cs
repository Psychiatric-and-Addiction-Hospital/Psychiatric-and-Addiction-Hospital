using Application.Common.Interfaces.HR.Manager;
using Application.Common.Responses;
using Application.DTOS.Request.HR.manager;
using Application.DTOS.Responses.HR.Manager;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.services.HR.Manager
{
    public class AssignDepartmentManagerService : IAssignDepartmentManager
    {
        private readonly AddIdentityDbContext _context;
        public AssignDepartmentManagerService(AddIdentityDbContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse<DepartmentManagerResponse>> AssignAsync(AssignDepartmentManagerRequest request, CancellationToken ct)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(x => x.Id == request.DepartmentId, ct);

            if (department == null)
                return ResponseFactory.Fail<DepartmentManagerResponse>("Department not found.");

            if (department.ManagerId != null)
                return ResponseFactory.Fail<DepartmentManagerResponse>("This department already has a manager.");

            var employee = await _context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.EmployeeId, ct);

            if (employee == null)
                return ResponseFactory.Fail<DepartmentManagerResponse>("Employee not found.");

            if (!employee.IsActive)
                return ResponseFactory.Fail<DepartmentManagerResponse>("Employee is not active.");

            if (employee.DepartmentId != department.Id)
                return ResponseFactory.Fail<DepartmentManagerResponse>("Employee does not belong to this department.");

            department.ManagerId = employee.Id;
            await _context.SaveChangesAsync(ct);

            var response = new DepartmentManagerResponse
            {
                DepartmentId = department.Id,
                DepartmentName = department.Name,
                ManagerId = employee.Id,
                ManagerName = employee.FullName
            };

            return ResponseFactory.Success(response, "Department manager assigned successfully.");
        }
    }
}
