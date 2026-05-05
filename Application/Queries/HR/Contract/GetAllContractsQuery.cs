using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.HR.Contract
{
    public record GetAllContractsQuery() : IRequest<BaseResponse<List<ContractResponse>>>;
}
