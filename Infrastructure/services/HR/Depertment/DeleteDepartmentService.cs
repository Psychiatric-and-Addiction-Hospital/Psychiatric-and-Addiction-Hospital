using Application.Common.Interfaces.HR;
using Application.Common.Interfaces.HR.Depertment;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Domain.Entites.ServicesModule;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.Depertment
{
    public class DeleteDepartmentService : IDeleteDepartment
    {
        private readonly AddIdentityDbContext _Context;
        public DeleteDepartmentService(AddIdentityDbContext context)
        {
            _Context = context;
        }

        public async Task<BaseResponse<DepartmentResponse>> DeleteDepartmentAsync(Guid Id, CancellationToken ct)
        {
            var department = await _Context.Departments
          .Include(d => d.Employees)
          .Include(d => d.Positions)
          .Include(d => d.JobPostings)
          .Include(d => d.Services)
          .FirstOrDefaultAsync(d => d.Id == Id, ct);

            if (department is null)
                return ResponseFactory.Fail<DepartmentResponse>("Department not found.");

            if (department.Employees.Any())
                return ResponseFactory.Fail<DepartmentResponse>("Cannot delete department because it has employees.");

            if (department.JobPostings.Any())
                return ResponseFactory.Fail<DepartmentResponse>("Cannot delete department because it has job postings.");

            if (department.Services.Any())
                return ResponseFactory.Fail<DepartmentResponse>("Cannot delete department because it has services.");

            _Context.Departments.Remove(department);
            await _Context.SaveChangesAsync(ct);

            return ResponseFactory.Success<DepartmentResponse>(null, "Department Deleted Successfully");
        }
    }
}
