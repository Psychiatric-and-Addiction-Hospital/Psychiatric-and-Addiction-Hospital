using Application.Common.Interfaces.HR.Payroll;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Application.Queries.HR.Payroll;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Payroll
{
    public class GetAllPayrollsHandler : IRequestHandler<GetAllPayrollsQuery, BaseResponse<List<PayrollResponse>>>
    {
        private readonly IPayrollService _payrollService;

        public GetAllPayrollsHandler(IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }

        public async Task<BaseResponse<List<PayrollResponse>>> Handle(GetAllPayrollsQuery request, CancellationToken ct)
        {
            return await _payrollService.GetAll(ct);
        }
    }
}
