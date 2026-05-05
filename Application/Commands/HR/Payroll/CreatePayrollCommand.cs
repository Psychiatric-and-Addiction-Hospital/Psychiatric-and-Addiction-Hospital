using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;

namespace Application.Commands.HR.Payroll
{
    public record CreatePayrollCommand(Guid EmployeeId, DateTime PaymentDate, decimal GrossPay, decimal Deductions, 
        decimal OvertimeRate, decimal OverSefit) : IRequest<BaseResponse<PayrollResponse>>;
}
