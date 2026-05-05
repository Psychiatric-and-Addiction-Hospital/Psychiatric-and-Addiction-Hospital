using Application.Common.Interfaces.HR.Contract;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Infrastructure.Persistence.Identity;

namespace Infrastructure.services.HR.Contract
{
    public class UpdateContractService : IUpdateContract
    {
        private readonly AddIdentityDbContext _Context;
        public UpdateContractService(AddIdentityDbContext context)
        {
            _Context = context;
        }

        public async Task<BaseResponse<ContractResponse>> UpdateContractAsync(Guid Id, Guid EmployeeId, DateTime StartDate, DateTime? EndDate, string Terms, decimal BaseSalary, CancellationToken ct)
        {
            var contract = await _Context.Contracts.FindAsync(Id, ct);
            if (contract is null)
                return ResponseFactory.Fail<ContractResponse>("Contract not found.");

            var employee = await _Context.Employees.FindAsync(EmployeeId);
            if (employee is null)
                return ResponseFactory.Fail<ContractResponse>("Employee not found.");

            contract.StartDate = StartDate;
            contract.EndDate = EndDate;
            contract.Terms = Terms;
            contract.BaseSalary = BaseSalary;
            contract.EmployeeId = EmployeeId;

            await _Context.SaveChangesAsync(ct);
            return ResponseFactory.Success(new ContractResponse
            {
                Id = contract.Id,
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                Terms = contract.Terms,
                BaseSalary = contract.BaseSalary,
                EmployeeId = contract.EmployeeId,
                EmployeeName = $"{employee.FirstName}{employee.LastName}"
            });
        }
    }
}
