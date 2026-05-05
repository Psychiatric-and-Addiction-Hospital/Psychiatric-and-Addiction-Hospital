using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;

namespace Application.Commands.HR.Contract
{
    public record CreateContractCommand(Guid EmployeeId, DateTime StartDate,DateTime? EndDate,string Terms,decimal BaseSalary) 
        : IRequest<BaseResponse<ContractResponse>>;

}
