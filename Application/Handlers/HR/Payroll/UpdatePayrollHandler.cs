using Application.Commands.HR.Payroll;
using Application.Common.Interfaces.HR.Payroll;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Payroll
{
    public class UpdatePayrollHandler : IRequestHandler<UpdatePayrollCommand, BaseResponse<PayrollResponse>>
    {
        private readonly IPayrollService _payrollService;

        public UpdatePayrollHandler(IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }

        public async Task<BaseResponse<PayrollResponse>> Handle(UpdatePayrollCommand request, CancellationToken ct)
        {
            return await _payrollService.UpdatePayroll(request.Id, request.GrossPay, request.Deductions, request.OvertimeRate, request.OverSefit, ct);
        }
    }
}
