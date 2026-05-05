using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;


namespace Application.Commands.HR.Contract
{
    public record UpdateContractCommand(Guid Id, Guid EmployeeId, DateTime StartDate, DateTime? EndDate, string Terms, decimal BaseSalary) 
        : IRequest<BaseResponse<ContractResponse>>;
}
