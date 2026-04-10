using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Payroll
{
    public interface IPayrollService
    {
        Task<BaseResponse<PayrollResponse>> CreatePayroll(Guid employeeId, DateTime paymentDate, decimal grossPay, decimal deductions, decimal overtimeRate, decimal overSefit, CancellationToken ct);
        Task<BaseResponse<PayrollResponse>> GetPayroll(Guid id, CancellationToken ct);
        Task<BaseResponse<List<PayrollResponse>>> GetAll(CancellationToken ct);
        Task<BaseResponse<PayrollResponse>> UpdatePayroll(Guid id, decimal grossPay, decimal deductions, decimal overtimeRate, decimal overSefit, CancellationToken ct);
        Task<BaseResponse<PayrollResponse>> DeletePayroll(Guid id, CancellationToken ct);
    }
}
