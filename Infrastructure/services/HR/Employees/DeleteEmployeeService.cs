using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Infrastructure.Persistence.Identity;
using Application.Common.Interfaces.HR.Employee;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.Employees
{
    public  class DeleteEmployeeService : IDeleteEmployee
    {
        private readonly AddIdentityDbContext _Context;
        public DeleteEmployeeService(AddIdentityDbContext context)
        {
            _Context = context;
        }

        public async Task<BaseResponse<EmployeeResponse>> DeleteEmployee(Guid Id, CancellationToken ct)
        {
            var employee = await _Context.Employees.FirstOrDefaultAsync(d => d.Id == Id);
            if (employee == null)
                return ResponseFactory.Fail<EmployeeResponse>("Employee not found.");
            _Context.Employees.Remove(employee);
            await _Context.SaveChangesAsync(ct);
            return ResponseFactory.Success<EmployeeResponse>( null, "Employee Deleted Successfully");
        }
    }
}
