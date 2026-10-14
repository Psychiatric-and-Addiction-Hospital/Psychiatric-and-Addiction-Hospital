using Application.Common.Interfaces.HR.Depertment;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.Depertment
{
    public class GetDepartmentByIdService : IGetDepartmentById
    {
        private readonly AddIdentityDbContext _context;

        public GetDepartmentByIdService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<DepartmentResponse>> GetDepartmentById(Guid id, CancellationToken ct)
        {
            var Department = await _context.Departments
                .Where(d => d.Id == id).Select(d => new DepartmentResponse
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description
                }).FirstOrDefaultAsync(ct);

            if (Department is null)
                return ResponseFactory.Fail<DepartmentResponse>("Department not found.");

            return ResponseFactory.Success(Department, "Department retrieved successfully.");
        }
    }

}
