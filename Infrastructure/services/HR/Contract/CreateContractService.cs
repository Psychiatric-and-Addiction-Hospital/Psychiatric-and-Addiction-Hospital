using Application.Common.Interfaces.HR.Contract;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.Contract
{
    public class CreateContractService : ICreateContract
    {
        private readonly AddIdentityDbContext _Context;
        public CreateContractService(AddIdentityDbContext context)
        {
            _Context = context;
        }

        public async Task<BaseResponse<ContractResponse>> CreateContractAsync(Guid EmployeeId, DateTime StartDate, DateTime? EndDate, string Terms, decimal BaseSalary, CancellationToken ct)
        {
            var employee = await _Context.Employees.FirstOrDefaultAsync(em=>em.Id==EmployeeId);
            if (employee is null)
                return ResponseFactory.Fail<ContractResponse>("Employee not found.");
            var contract = new Domain.Entites.HR.Contract
            {
                StartDate = StartDate,
                EndDate = EndDate,
                Terms = Terms,
                BaseSalary = BaseSalary,
                EmployeeId = EmployeeId
            };

            await _Context.Contracts.AddAsync(contract, ct);
            await _Context.SaveChangesAsync(ct);

            return ResponseFactory.Success(new ContractResponse
            {
                Id = contract.Id,
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                Terms = contract.Terms,
                BaseSalary = contract.BaseSalary,
                EmployeeId = contract.EmployeeId,
                EmployeeName = $"{employee.FirstName} {employee.LastName}"
            });
        }

      
    }
}
