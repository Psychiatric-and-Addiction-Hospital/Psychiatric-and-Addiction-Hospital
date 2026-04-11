using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;

namespace Application.Queries.HR.Payroll
{
    public record GetPayrollByIdQuery(Guid Id) : IRequest<BaseResponse<PayrollResponse>>;
}
