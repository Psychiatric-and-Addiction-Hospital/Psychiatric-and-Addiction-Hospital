using Application.Common.Interfaces.HR.Contract;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Application.DTOS.Responses.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.Contract
{
    public class GetAllContractService : IGetAllContracts
    {
        private readonly AddIdentityDbContext _Context;
        public GetAllContractService(AddIdentityDbContext context)
        {
            _Context = context;
        }
        public async Task<BaseResponse<List<ContractResponse>>> GetAllContractsAsync(CancellationToken ct)
        {
            var Contract = await _Context.Contracts.Select(c => new ContractResponse
            {
                Id = c.Id,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                Terms = c.Terms,
                BaseSalary = c.BaseSalary,
                EmployeeId = c.EmployeeId,
                EmployeeName =$"{c.Employee.FirstName} {c.Employee.LastName}",
            }).ToListAsync(ct);
            if (Contract == null || Contract.Count == 0)
            {
                return ResponseFactory.Fail<List<ContractResponse>>("No Contract found.");
            }

            return ResponseFactory.Success(Contract, "All Contract have been fetched successfully.");
        }
    }
}
