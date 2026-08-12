using Application.Common.Constants;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Common;
using Application.Common.Interfaces.Doctores.ManagementDoctor;
using Application.Common.Interfaces.HR.Employee;
using Application.Common.Responses;
using Application.DTOS.Request.HR.Employee;
using Application.DTOS.Responses.HR.Employee;
using Domain.Entites;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.services.HR.Employee
{
    public class HireEmployeeService : IHireEmployee
    {
        private readonly AddIdentityDbContext _context;

        private readonly UserManager<AppUser> _userManager;

        private readonly IHireEmployeeVaildation _validation;

        private readonly IEmployeeCodeGenerator _employeeCodeGenerator;

        private readonly IDoctorProfileCreator _doctorProfileCreator;

        private readonly IEmployeeWelcomeEmailService _credentialEmail;

        private readonly IConfiguration _configuration;
        public HireEmployeeService(AddIdentityDbContext context,
            IHireEmployeeVaildation Vaildation,
            UserManager<AppUser> userManager,
            IEmployeeCodeGenerator employeeCodeGenerator,
            IDoctorProfileCreator doctorProfileCreator,
            IEmployeeWelcomeEmailService credentialEmail, IConfiguration onfiguration)
        {
            _context = context;
            _validation = Vaildation;
            _userManager = userManager;
            _employeeCodeGenerator = employeeCodeGenerator;
            _doctorProfileCreator = doctorProfileCreator;
            _credentialEmail = credentialEmail;
            _configuration = onfiguration;
        }
        public async Task<BaseResponse<EmployeeResponse>> HireAsync(HireEmployeeRequest request, CancellationToken ct)
        {
            var validation = await _validation.ValidateHireAsync(request, ct);

            if (!validation.Success)
                return ResponseFactory.Fail<EmployeeResponse>(validation.Message, validation.Errors);

            var contract = validation.Data!;

            var candidate = contract.Offer.Application.Candidate;

            var employeeCode = await _employeeCodeGenerator.GenerateAsync(request.Role, ct);

            AppUser? appUser = null;

            await using var transaction = await _context.Database.BeginTransactionAsync(ct);
            try
            {

                if (string.IsNullOrEmpty(candidate.AppUserId))
                {
                    return ResponseFactory.Fail<EmployeeResponse>(
                        "Candidate does not have an account.");
                }

                appUser = await _userManager.FindByIdAsync(candidate.AppUserId);

                if (appUser == null)
                    return ResponseFactory.Fail<EmployeeResponse>("Candidate account was not found.");

                var currentRoles = await _userManager.GetRolesAsync(appUser);

                if (currentRoles.Contains(Roles.Candidate))
                {
                    var removeResult =
                        await _userManager.RemoveFromRoleAsync(appUser, Roles.Candidate);

                    if (!removeResult.Succeeded)
                    {
                        var errors = removeResult.Errors.Select(x => x.Description).ToList();
                        return ResponseFactory.Fail<EmployeeResponse>("Failed to update candidate role.", errors);
                    }
                }

                var roleResult = await _userManager.AddToRoleAsync(appUser, request.Role);

                if (!roleResult.Succeeded)
                {
                    var errors = roleResult.Errors.Select(x => x.Description).ToList();
                    return ResponseFactory.Fail<EmployeeResponse>("Failed to assign employee role.", errors);
                }

                var employee = new Domain.Entites.HR.Employee
                {
                    EmployeeCode = employeeCode,
                    FirstName = candidate.FirstName.Trim(),
                    LastName = candidate.LastName.Trim(),
                    Email = candidate.Email.Trim(),
                    PhoneNumber = candidate.PhoneNumber.Trim(),
                    NationalId = candidate.NationalId,
                    DateOfBirth = candidate.DateOfBirth,
                    HireDate = contract.StartDate,
                    EmploymentStatus = EmploymentStatus.Active,
                    DepartmentId = request.DepartmentId,
                    PositionId = request.PositionId,
                    ShiftId = request.ShiftId,
                    ManagerId = request.ManagerId,
                    IsActive = true,
                    AppUserId = appUser.Id
                };

                await _context.Employees.AddAsync(employee, ct);

                if (request.Role == Roles.Doctor)
                {
                    await _doctorProfileCreator.CreateAsync(
                        appUser,
                        employee,
                        request,
                        ct);
                }

                contract.Status = ContractStatus.Active;
                contract.Offer.Status = OfferStatus.Accepted;
                contract.Offer.Application.Status = ApplicationStatus.Hired;

                await _context.SaveChangesAsync(ct);

                await transaction.CommitAsync(ct);

                await _credentialEmail.SendAsync(
                  employee.Email, employee.FullName, employee.EmployeeCode,
                  contract.Offer.Application.JobPosting.Position.Name,
                  contract.Offer.Application.JobPosting.Position.Department.Name,
                  _configuration["Frontend:LoginUrl"]!, ct);

                return ResponseFactory.Success(new EmployeeResponse
                {
                    Id = employee.Id,
                    EmployeeCode = employee.EmployeeCode,
                    FullName = employee.FullName,
                    Email = employee.Email
                }, "Employee hired successfully.");
            }

            catch
            {
                await transaction.RollbackAsync(ct);
                throw;
            }
        }
    }
}
