using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;

namespace Application.Commands.HR.Payroll
{
    public record DeletePayrollCommand(Guid Id) : IRequest<BaseResponse<PayrollResponse>>;
}
