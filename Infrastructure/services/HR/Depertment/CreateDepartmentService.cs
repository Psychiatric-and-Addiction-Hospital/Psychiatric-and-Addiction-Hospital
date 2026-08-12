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
        public async Task<BaseResponse<DepartmentResponse>> CreateAsync(string name, string description, CancellationToken ct)
        {
            name = name.Trim();

            bool exists = await _context.Departments
                .AnyAsync(x => x.Name == name, ct);

            if (exists)
            {
                return ResponseFactory.Fail<DepartmentResponse>(
                    "Department already exists.");
            }

            var department = new Department
            {
                Name = name,
                Description = description.Trim()
            };

            await _context.Departments.AddAsync(department, ct);

            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(
                new DepartmentResponse
                {
                    Id = department.Id,
                    Name = department.Name,
                    Description = department.Description
                },
                "Department created successfully.");

        }
    }
}
