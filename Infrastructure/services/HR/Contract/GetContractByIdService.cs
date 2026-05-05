using Application.Common.Interfaces.HR.Contract;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.Contract
{
    public class GetContractByIdService : IGetContractById
    {
        private readonly AddIdentityDbContext _Context;
        public GetContractByIdService(AddIdentityDbContext context)
        {
            _Context = context;
        }
        public async Task<BaseResponse<ContractResponse>> GetContractByIdAsync(Guid contractId, CancellationToken ct)
        {
            var contract = await _Context.Contracts.FirstOrDefaultAsync(c=>c.Id==contractId);
                if (contract is null)
                    return ResponseFactory.Fail<ContractResponse>("Contract not found.");

            return ResponseFactory.Success(new ContractResponse
            {
                Id = contract.Id,
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                Terms = contract.Terms,
                BaseSalary = contract.BaseSalary,
                EmployeeId = contract.EmployeeId
            }, "Contract retrieved successfully.");
        }
    }
}
