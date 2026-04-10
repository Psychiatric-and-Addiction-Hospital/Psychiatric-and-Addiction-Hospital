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

        public async Task<BaseResponse<DepertmentResponse>> DeleteDepartment(Guid Id, CancellationToken ct)
        {
            var department = await _Context.Departments.FirstOrDefaultAsync(d => d.Id == Id);
            if (department == null)
                return ResponseFactory.Fail<DepertmentResponse>("Department not found.");
            _Context.Departments.Remove(department);
            await _Context.SaveChangesAsync(ct);
            return ResponseFactory.Success<DepertmentResponse>(null, "Depertment Deleted Successfully");
        }
    }
}
