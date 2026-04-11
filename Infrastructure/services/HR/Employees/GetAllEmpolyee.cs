using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Domain.Entites.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.Employees
{
    public class GetAllEmpolyee : Application.Common.Interfaces.HR.Employee.IGetAllEmployee
    {
        private readonly AddIdentityDbContext _Context;
        public GetAllEmpolyee(AddIdentityDbContext context)
        {
            _Context = context;
        }

   
  
        public async Task<BaseResponse<List<EmployeeResponse>>> GetAllEmployee(CancellationToken ct)
        {
            var employees = await _Context.Employees.Select(d => new EmployeeResponse
            {
                Id = d.Id,
                EmployeeCode = d.EmployeeCode,
                FullName = $"{d.FirstName} {d.LastName}",
                Email = d.Email,
                DepartmentId = d.DepartmentId,
                IsActive = d.IsActive
            }).ToListAsync(ct);

            return ResponseFactory.Success(employees, employees.Any() ? "Employees retrieved successfully" : "No employees found");
        }

    }
}
