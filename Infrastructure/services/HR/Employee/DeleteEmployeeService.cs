using Application.Common.Interfaces.HR.Employee;
using Application.Common.Responses;
using Application.DTOS.Request.HR.Employee;
using Domain.Entites;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.services.HR.Employee
{
    public class DeleteEmployeeService : IDeleteEmployee
    {
        private readonly AddIdentityDbContext _context;
        private readonly IDeleteEmployeeValidation _validation;
        private readonly UserManager<AppUser> _userManager;
        public DeleteEmployeeService(AddIdentityDbContext context, IDeleteEmployeeValidation validation, UserManager<AppUser> userManager)
        {
            _context = context;
            _validation = validation;
            _userManager = userManager;
        }
        public async Task<BaseResponse<bool>> DeleteAsync(DeleteEmployeeRequest request, CancellationToken ct)
        {
            var validation = await _validation.ValidateAsync(request, ct);
            if (!validation.Success)
                return ResponseFactory.Fail<bool>(validation.Message, validation.Errors);

            var employee = validation.Data!;
            await using var transaction = await _context.Database.BeginTransactionAsync(ct);
            try
            {


                employee.IsActive = false;

                employee.EmploymentStatus = EmploymentStatus.Terminated;

                employee.TerminationDate = DateTime.UtcNow;

                employee.TerminationReason = request.Reason?.Trim();

                if (employee.AppUser != null)
                {
                    employee.AppUser.IsActive = false;

                    employee.AppUser.LockoutEnabled = true;

                    employee.AppUser.LockoutEnd = DateTimeOffset.MaxValue;

                    var updateResult = await _userManager.UpdateAsync(employee.AppUser);

                    if (!updateResult.Succeeded)
                        return ResponseFactory.Fail<bool>(
                            "Failed to deactivate employee account.", updateResult.Errors.Select(x => x.Description).ToList());
                    
                }

                await _context.SaveChangesAsync(ct);

                await transaction.CommitAsync(ct);

                return ResponseFactory.Success(true, "Employee has been terminated successfully.");
            }
            catch
            {
                await transaction.RollbackAsync(ct);
                throw;
            }
        }
    }
}
