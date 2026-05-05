using Application.Common.Interfaces.HR.Contract;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Application.Queries.HR.Contract;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Contract
{
    public class GetAllContractsHandler : IRequestHandler<GetAllContractsQuery, BaseResponse<List<ContractResponse>>>
    {
        private readonly IGetAllContracts _getAllContracts;
        public GetAllContractsHandler(IGetAllContracts getAllContracts)
        {
            _getAllContracts = getAllContracts;
        }
        public async Task<BaseResponse<List<ContractResponse>>> Handle(GetAllContractsQuery request, CancellationToken ct)
        {
            return await _getAllContracts.GetAllContractsAsync(ct);
        }
    }
}
