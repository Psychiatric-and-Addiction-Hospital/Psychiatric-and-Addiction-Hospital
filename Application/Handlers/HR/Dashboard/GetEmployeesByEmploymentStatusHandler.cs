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
    public class GetEmployeesByEmploymentStatusHandler : IRequestHandler<GetEmployeesByEmploymentStatusQuery, BaseResponse<List<EmployeesByEmploymentStatusResponse>>>
    {
        private readonly IGetEmployeesByEmploymentStatus _service;
        public GetEmployeesByEmploymentStatusHandler(IGetEmployeesByEmploymentStatus service)
        {
            _service = service;
        }
        public async Task<BaseResponse<List<EmployeesByEmploymentStatusResponse>>> Handle(GetEmployeesByEmploymentStatusQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAsync(cancellationToken);
        }
    }
}
