using Application.Common.Interfaces.HR.Depertment;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.services.HR.Depertment
{
    public class UpdateDepartmentService : IUpdateDepartment
    {
        private readonly AddIdentityDbContext _Context;
        public UpdateDepartmentService(AddIdentityDbContext context)
        {
            _Context = context;
        }

        public async Task<BaseResponse<DepertmentResponse>> UpdateAsync(Guid id, string name, string description, CancellationToken ct)
        {

            var department = await _Context.Departments.FirstOrDefaultAsync(d => d.Id == id);
            if (department == null)
                return ResponseFactory.Fail<DepertmentResponse>("Department Not Found");
            department.Name = name;
            department.Description = description;
            await _Context.SaveChangesAsync(ct);
            return ResponseFactory.Success(
                new DepertmentResponse
                {
                    Id = department.Id,
                    Name = department.Name,
                    Description = department.Description
                },
                "Department Updated Successfully"
                );

        }
    }
}
