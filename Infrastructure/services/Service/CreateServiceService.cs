using Application.Common.Interfaces.Services;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Domain.Entites.ServicesModule;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.services.Service
{
    public class CreateServiceService : ICreateService
    {
        private readonly AddIdentityDbContext _context;

        public CreateServiceService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<ServiceResponse>> CreateAsync(string name, string description, Guid deptId, CancellationToken ct)
        {
            var service = new Domain.Entites.ServicesModule.Service
            {
                Name = name,
                Description = description,
                DepartmentId = deptId
            };

            await _context.Services.AddAsync(service, ct);
            await _context.SaveChangesAsync(ct);

            var serviceWithDept = await _context.Services
              .Include(s => s.Department)
              .FirstOrDefaultAsync(s => s.Id == service.Id, ct);
            return ResponseFactory.Success(new ServiceResponse
            {
                Id = serviceWithDept.Id,
                Name = serviceWithDept.Name,
                Description = serviceWithDept.Description,
                DepartmentName = serviceWithDept.Department.Name
            }, "Service created successfully");
        }

    }
}
