using Application.Common.Interfaces.HR.Employee;
using Application.Common.Responses;
using Application.DTOS.Request.HR.Employee;
using Domain.Entites;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.Employee
{
    public class RestoreEmployeeService : IRestoreEmployee
    {
        private readonly AddIdentityDbContext _context;
        private readonly IRestoreEmployeeValidation _validation;
        private readonly UserManager<AppUser> _userManager;

        public RestoreEmployeeService(AddIdentityDbContext context, IRestoreEmployeeValidation validation, UserManager<AppUser> userManager)
        {
            _context = context;
            _validation = validation;
            _userManager = userManager;
        }

        public async Task<BaseResponse<bool>> RestoreAsync(RestoreEmployeeRequest request, CancellationToken ct)
        {
            var validation = await _validation.ValidateAsync(request, ct);

            if (!validation.Success)
                return ResponseFactory.Fail<bool>(validation.Message, validation.Errors);

            var employee = validation.Data!;

            await using var transaction = await _context.Database.BeginTransactionAsync(ct);

            try
            {
                employee.IsActive = true;
                employee.EmploymentStatus = EmploymentStatus.Active;

                employee.TerminationDate = null;

                employee.TerminationReason = null;

                if (employee.AppUser != null)
                {
                    employee.AppUser.IsActive = true;

                    employee.AppUser.LockoutEnabled = false;

                    employee.AppUser.LockoutEnd = null;

                    var result = await _userManager.UpdateAsync(employee.AppUser);

                    if (!result.Succeeded)
                        return ResponseFactory.Fail<bool>("Failed to restore employee account.",
                            result.Errors.Select(x => x.Description).ToList());

                }

                await _context.SaveChangesAsync(ct);

                await transaction.CommitAsync(ct);

                return ResponseFactory.Success(true, "Employee restored successfully.");
            }
            catch
            {
                await transaction.RollbackAsync(ct);
                throw;
            }
        }
    }
}