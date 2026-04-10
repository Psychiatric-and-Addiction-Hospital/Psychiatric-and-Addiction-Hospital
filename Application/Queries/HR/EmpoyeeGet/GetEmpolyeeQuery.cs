using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.HR.EmpoyeeGet
{
    public record GetEmployee1query() : IRequest<BaseResponse<List<EmployeeResponse>>>;
}
