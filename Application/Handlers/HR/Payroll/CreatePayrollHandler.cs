using Application.Commands.HR.Payroll;
using Application.Common.Interfaces.HR.Payroll;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Payroll
{
    public class CreatePayrollHandler : IRequestHandler<CreatePayrollCommand, BaseResponse<PayrollResponse>>
    {
        private readonly IPayrollService _payrollService;

        public CreatePayrollHandler(IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }

        public async Task<BaseResponse<PayrollResponse>> Handle(CreatePayrollCommand request, CancellationToken ct)
        {
            return await _payrollService.CreatePayroll(request.EmployeeId, request.PaymentDate, request.GrossPay, request.Deductions, request.OvertimeRate, request.OverSefit, ct);
        }
    }
}
