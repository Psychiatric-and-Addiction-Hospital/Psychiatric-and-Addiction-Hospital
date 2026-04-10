using Application.Common.Interfaces.HR.Depertment;
using Application.Common.Responses;
<<<<<<< HEAD:Infrastructure/services/HR/Depertment/CreateDepartmentService.cs
using Application.DTOS.Responses.HR;
=======
using Application.DTOS.Responses;

>>>>>>> c64fe1ca6f5215cfb1d78b61617c42a22b944b0d:Infrastructure/services/Depertment/CreateDepartmentService.cs
using Domain.Entites.HR;

using Domain.Entites.ServicesModule;
using Infrastructure.Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.Depertment
{
    public class CreateDepartmentService : ICreateDepartment
    {
        private readonly AddIdentityDbContext _context;

        public CreateDepartmentService(AddIdentityDbContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse<DepertmentResponse>> CreateAsync(string name, string description, CancellationToken ct)
        {
            var dept = new Department
            {
                Name = name,
                Description = description
            };

            await _context.Departments.AddAsync(dept, ct);
            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(new DepertmentResponse
            {
                Id=dept.Id,
                Name = dept.Name,
                Description = dept.Description
            }, "Department created successfully");

        }
    }
}
