using Application.Common.Interfaces.Services;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.services.Service
{
    public class UpdateServiceService : IUpdateService
    {
        private readonly AddIdentityDbContext _Context;

        public UpdateServiceService(AddIdentityDbContext context)
        {
            _Context = context;
        }

        public async Task<BaseResponse<ServiceResponse>> UpdateAsync(Guid id, string name, string description, Guid DepartmentId, CancellationToken ct)
        {
            var service = await _Context.Services.FirstOrDefaultAsync(d => d.Id == id);
            if (service == null)
                return ResponseFactory.Fail<ServiceResponse>("Service Not Found");
            service.Name = name;
            service.Description = description;
            service.DepartmentId = DepartmentId;
            await _Context.SaveChangesAsync(ct);
            var serviceWithDept = await _Context.Services
             .Include(s => s.Department)
             .FirstOrDefaultAsync(s => s.Id == service.Id, ct);
            return ResponseFactory.Success(
                 new ServiceResponse
                 {
                     Id = serviceWithDept.Id,
                     Name = serviceWithDept.Name,
                     Description = serviceWithDept.Description,
                     DepartmentName = serviceWithDept.Department.Name
                 },
                "Service Updated Successfully");




        }


    }
}
