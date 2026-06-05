using Application.Common.Interfaces.HR.Depertment;
using Application.Common.Responses;

using Application.DTOS.Responses.HR;
using Domain.Entites.HR;

using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.services.Depertment
{
    public class CreateDepartmentService : ICreateDepartment
    {
        private readonly AddIdentityDbContext _context;

        public CreateDepartmentService(AddIdentityDbContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse<DepertmentResponse>> CreateAsync(Guid managerid, string name, string description, CancellationToken ct)
        {
            var manger = await _context.Employees.FirstOrDefaultAsync(em => em.Id == managerid);
            if (manger is null)
                return ResponseFactory.Fail<DepertmentResponse>("ManagerId not found.");
            var dept = new Department
            {
                Name = name,
                Description = description
                ,
                ManagerId = managerid
            };

            await _context.Departments.AddAsync(dept, ct);
            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(new DepertmentResponse
            {
                Id = dept.Id,
                Name = dept.Name,
                Description = dept.Description
            }, "Department created successfully");

        }
    }
}
