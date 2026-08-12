using Application.Common.Constants;
using Application.Common.Interfaces.HR.Employee;
using Application.Common.Responses;
using Application.DTOS.Request.HR.Employee;
using Azure.Core;
using Domain.Entites.HR;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ContractEntity = Domain.Entites.HR.Contract;

namespace Infrastructure.services.HR.Employee
{
    public class HireEmployeeVaildationService : IHireEmployeeVaildation
    {
        private readonly AddIdentityDbContext _context;
        public readonly RoleManager<IdentityRole> _roleManager;
        public HireEmployeeVaildationService(AddIdentityDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }
        public async Task<BaseResponse<ContractEntity>> ValidateHireAsync(HireEmployeeRequest request, CancellationToken ct)
        {
            var contract = await _context.Contracts
                   .Include(x => x.Offer)
                       .ThenInclude(x => x.Application)
                           .ThenInclude(x => x.Candidate)
                   .FirstOrDefaultAsync(x => x.Id == request.ContractId, ct);

            if (contract == null)
                return ResponseFactory.Fail<ContractEntity>("Contract not found.");

            if (contract.Status != ContractStatus.Signed)
                return ResponseFactory.Fail<ContractEntity>("Only signed contracts can hire employees.");


            if (contract.Offer.Status != OfferStatus.Accepted)
                return ResponseFactory.Fail<ContractEntity>("Offer must be accepted.");


            if (contract.Offer.Application.Status != ApplicationStatus.Offered)
                return ResponseFactory.Fail<ContractEntity>("Only offered applications can be hired.");

            if (contract.Offer.Application.Candidate == null)
                return ResponseFactory.Fail<ContractEntity>("Candidate not found.");

            var department = await GetDepartment(request.DepartmentId, ct);

            if (!department)
                return ResponseFactory.Fail<ContractEntity>("Department not found.");

            var position = await GetPosition(request.PositionId, ct);

            if (!position)
                return ResponseFactory.Fail<ContractEntity>("Position not found.");

            var shift = await GetShift(request.ShiftId, ct);

            if (!shift)
                return ResponseFactory.Fail<ContractEntity>("Shift not found.");

            if (request.ManagerId.HasValue)
            {
                var manager = await GetManager(request.ManagerId.Value, ct);

                if (!manager)
                    return ResponseFactory.Fail<ContractEntity>("Manager not found.");
            }
            var emailExists = await _context.Employees.AnyAsync(x => x.Email == contract.Offer.Application.Candidate.Email, ct);

            if (emailExists)
                return ResponseFactory.Fail<ContractEntity>("Employee with this email already exists.");

            var nationalIdExists = await _context.Employees
                .AnyAsync(x => x.NationalId == contract.Offer.Application.Candidate.NationalId, ct);

            if (nationalIdExists)
                return ResponseFactory.Fail<ContractEntity>("Employee with this national ID already exists.");

            var roleExists = await _roleManager.RoleExistsAsync(request.Role);

            if (!roleExists)
                return ResponseFactory.Fail<ContractEntity>("Role not found.");

            if (request.Role == Roles.Doctor)
            {
                if (string.IsNullOrWhiteSpace(request.DoctorProfile?.Specialization))
                    return ResponseFactory.Fail<ContractEntity>("Specialization is required for doctors.");

                if (string.IsNullOrWhiteSpace(request.DoctorProfile?.Degree))
                    return ResponseFactory.Fail<ContractEntity>("Degree is required for doctors.");

                if (string.IsNullOrWhiteSpace(request.DoctorProfile?.LicenseNumber))
                    return ResponseFactory.Fail<ContractEntity>("License number is required for doctors.");
            }


            return ResponseFactory.Success(contract, "Validation succeeded.");


        }

        private async Task<bool> GetDepartment(Guid DepartmentId, CancellationToken ct)
        {
            return await _context.Departments.AnyAsync(x => x.Id == DepartmentId && x.IsActive, ct);
        }

        private async Task<bool> GetPosition(Guid PositionId, CancellationToken ct)
        {
            return await _context.Positions.AnyAsync(x => x.Id == PositionId && x.IsActive, ct);
        }

        private async Task<bool> GetShift(Guid ShiftId, CancellationToken ct)
        {
            return await _context.Shifts.AnyAsync(x => x.Id == ShiftId && x.IsActive, ct);
        }

        private async Task<bool> GetManager(Guid? EmployeeId, CancellationToken ct)
        {
            return await _context.Employees.AnyAsync(x => x.Id == EmployeeId && x.IsActive, ct);
        }
    }
}
