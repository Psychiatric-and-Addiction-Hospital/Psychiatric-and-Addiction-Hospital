using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.HR.Employee
{
    public record CreateEmployeeCommand 
    (
       string UserId ,
       string EmployeeCode ,
       string FirstName ,
       string LastName ,
       string Email ,      
       Guid DepartmentId 
    ): IRequest<BaseResponse<EmployeeResponse>>;
}
