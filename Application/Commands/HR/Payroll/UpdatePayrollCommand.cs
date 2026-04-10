using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;

namespace Application.Commands.HR.Payroll
{
    public record UpdatePayrollCommand(Guid Id, decimal GrossPay, decimal Deductions, decimal OvertimeRate, decimal OverSefit) : IRequest<BaseResponse<PayrollResponse>>;
}
