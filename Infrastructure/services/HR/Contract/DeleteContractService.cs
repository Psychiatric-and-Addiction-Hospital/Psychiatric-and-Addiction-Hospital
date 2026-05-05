using Application.Common.Interfaces.HR.Contract;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Infrastructure.Persistence.Identity;

namespace Infrastructure.services.HR.Contract
{
    public class DeleteContractService : IDeleteContract
    {
        private readonly AddIdentityDbContext _Context;
        public DeleteContractService(AddIdentityDbContext context)
        {
            _Context = context;
        }
        public async Task<BaseResponse<ContractResponse>> DeleteContractAsync(Guid contractId, CancellationToken ct)
        {
            var contract = await _Context.Contracts.FindAsync(contractId);
            if (contract is null)
                return ResponseFactory.Fail<ContractResponse>("Contract not found.");

            _Context.Contracts.Remove(contract);
            await _Context.SaveChangesAsync(ct);

            return ResponseFactory.Success<ContractResponse>(null, "Contract deleted successfully.");
        }
    }
}
