using Application.Common.Responses;
using Application.DTOS.Request.HR.Employee;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.HR.Employee
{
    public record DeleteEmployeeCommand(DeleteEmployeeRequest request) : IRequest<BaseResponse<bool>>;
}
