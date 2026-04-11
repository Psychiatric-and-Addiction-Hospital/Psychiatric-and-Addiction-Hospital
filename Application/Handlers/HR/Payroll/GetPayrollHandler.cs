using Application.Common.Interfaces.HR.Payroll;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Application.Queries.HR.Payroll;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Payroll
{
    public class GetPayrollHandler : IRequestHandler<GetPayrollByIdQuery, BaseResponse<PayrollResponse>>
    {
        private readonly IPayrollService _payrollService;

        public GetPayrollHandler(IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }

        public async Task<BaseResponse<PayrollResponse>> Handle(GetPayrollByIdQuery request, CancellationToken ct)
        {
            return await _payrollService.GetPayroll(request.Id, ct);
        }
    }
}
