using Application.Common.Responses;
using Application.DTOS.Responses;
using Application.DTOS.Responses.HR;
using Domain.Entites.HR;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Employee
{
    public interface ICreateEmployee

    {
     
        
        Task<BaseResponse<EmployeeResponse>> CreateAsync(string UserId, string EmployeeCode, string FirstName, string LastName, string Email, Guid DepartmentId, CancellationToken ct)
        {
throw new NotImplementedException();
    }
    }
}
