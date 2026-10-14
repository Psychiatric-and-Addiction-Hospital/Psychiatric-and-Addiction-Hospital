using Application.Common.Interfaces.HR.Dashboard;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Dashboard;
using Application.Queries.HR.Dashboard;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Dashboard
{
    public class GetEmployeesByDepartmentHandler
        : IRequestHandler<GetEmployeesByDepartmentQuery, BaseResponse<List<EmployeesByDepartmentResponse>>>
    {
        private readonly IGetEmployeesByDepartment _service;

        public GetEmployeesByDepartmentHandler(IGetEmployeesByDepartment service)
        {
            _service = service;
        }

        public async Task<BaseResponse<List<EmployeesByDepartmentResponse>>> Handle(
            GetEmployeesByDepartmentQuery request,
            CancellationToken cancellationToken)
        {
            return await _service.GetAsync(cancellationToken);
        }
    }
}
