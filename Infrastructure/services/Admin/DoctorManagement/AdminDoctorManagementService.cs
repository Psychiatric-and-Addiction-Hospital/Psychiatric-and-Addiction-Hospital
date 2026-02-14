using Application.Common.Interfaces.Admin;
using Application.Common.Interfaces.Authentication;
using Domain.Entites;
using Domain.Enums;
using FluentResults;
using Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.Admin.DoctorManagement
{
    public class AdminDoctorManagementService: IAdminDoctorManagement
    {
        private readonly AddIdentityDbContext _Context;
        private readonly IEmailService _emailService;
        private readonly UserManager<AppUser> _userManager;
        public AdminDoctorManagementService(AddIdentityDbContext context, IEmailService emailService,
            UserManager<AppUser> userManager)
        {
            _Context = context;
            _emailService = emailService;
            _userManager = userManager;
        }

        #region GetApprovedDoctorsAsync
        public async Task<List<DoctorApplication>> GetApprovedDoctorsAsync(CancellationToken ct)
        {
            return await _Context.DoctorApplications.Where(d => d.Status == Status.Approved)
                .OrderByDescending(d => d.SubmittedAt).ToListAsync(ct);

        }
        #endregion

        #region GetPendingDoctorsAsync
        public async Task<List<DoctorApplication>> GetPendingDoctorsAsync(CancellationToken ct)
        {
            return await _Context.DoctorApplications.Where(d => d.Status == Status.Pending)
                 .OrderByDescending(d => d.SubmittedAt).ToListAsync(ct);
        }
        #endregion

        #region ApplyForDoctorAsync
        public async Task<Result<string>> ApplyForDoctorAsync(Guid ApplicationId, CancellationToken CT)
        {
            var application = await _Context.DoctorApplications.FindAsync(ApplicationId);
            if (application == null)
                return Result.Fail("Application not found.");

            if (application.Status == Status.Approved)
                return Result.Fail("Already approved.");

            application.Status = Status.Approved;
            await _Context.SaveChangesAsync(CT);
            var password = $"Doctor@{Guid.NewGuid().ToString("N").Substring(0, 8)}{application.FullName}";
            //new Random().Next(1000, 9999)
            var user = new AppUser
            {
                Email = application.Email,
                UserName = application.Email,
                PhoneNumber = application.PhoneNumber,
                FirstName = application.FullName.Split(" ")[0],
                LastName = application.FullName.Split(" ").Last(),
                RoleType = RoleType.Doctor,
                IsActive = true
            };
            var createResult = await _userManager.CreateAsync(user, password);
            if (!createResult.Succeeded)
            {
                if (createResult.Errors.Any(e => e.Code == "DuplicateUserName"))
                {
                    return Result.Fail("This username already exists.");
                }
                return Result.Fail("Failed to create user account for the doctor.");
            }
            await _userManager.AddToRoleAsync(user, "Doctor");

            var profile = new DoctorProfile
            {
                UserId = user.Id,
                FullName = application.FullName,
                Specialization = application.Specialization,
                PhoneNumber = application.PhoneNumber,
                CreatedAt = DateTime.UtcNow
            };
            _Context.DoctorProfiles.Add(profile);
            await _Context.SaveChangesAsync(CT);

            await _emailService.SendAsync(application.Email, "Doctor Application Approved",
                $"Congratulations! Your doctor application has been approved." +
                $" You can now log in with the following credentials" +
                $":\t\tEmail: {application.Email}\tPassword: {password}");
            return Result.Ok("Doctor application approved and email sent to the applicant.");
        }
        #endregion

        #region RejectDoctorAsync
        public async Task<Result<string>> RejectDoctorAsync(Guid applicationId, string reason, CancellationToken ct)
        {
            var result = await _Context.DoctorApplications.FindAsync(applicationId);

            if (result == null)
                return Result.Fail("Application not found");

            if (result.Status == Status.Rejected)
                return Result.Fail("Already rejected");

            result.Status = Status.Rejected;
            await _Context.SaveChangesAsync(ct);

            await _emailService.SendAsync(
            result.Email,
            "Doctor Application Rejected",
            $"Sorry, your application has been rejected.\nReason: {reason}");

            return Result.Ok("Application rejected.");
        }
        #endregion
    }
}
