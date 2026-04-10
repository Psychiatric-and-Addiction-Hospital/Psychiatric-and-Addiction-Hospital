using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.HR.Payroll
{
    public record GetAllPayrollsQuery() : IRequest<BaseResponse<List<PayrollResponse>>>;
}
