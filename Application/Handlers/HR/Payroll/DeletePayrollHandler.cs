using Application.Commands.HR.Payroll;
using Application.Common.Interfaces.HR.Payroll;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Payroll
{
    public class DeletePayrollHandler : IRequestHandler<DeletePayrollCommand, BaseResponse<PayrollResponse>>
    {
        private readonly IPayrollService _payrollService;

        public DeletePayrollHandler(IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }

        public async Task<BaseResponse<PayrollResponse>> Handle(DeletePayrollCommand request, CancellationToken ct)
        {
            return await _payrollService.DeletePayroll(request.Id, ct);
        }
    }
}
