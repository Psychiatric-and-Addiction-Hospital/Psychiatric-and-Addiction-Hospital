using Application.Common.Interfaces.Admin;
using Application.Common.Interfaces.Authentication;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Domain.Entites;
using Domain.Entites.DoctorsModule;
using Domain.Enums;
using Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.services.Admin.DoctorManagement
{
    // Lightweight implementation: DoctorApplication entity/DbSet is not present in the current DbContext.
    // Provide stubs that return empty results or not-implemented messages so DI can construct the service
    // and handlers that depend on it will not fail at startup.
    public class AdminDoctorManagementService : IAdminDoctorManagement
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

        public Task<BaseResponse<List<PendingDoctorApplicationResponse>>> GetPendingDoctorsAsync(CancellationToken ct)
        {
            // Return empty list to avoid runtime errors when DoctorApplication entity isn't available
            var empty = new List<PendingDoctorApplicationResponse>();
            return Task.FromResult(ResponseFactory.Success(empty, "No pending doctor applications available in this build."));
        }

        public Task<BaseResponse<List<ApprovedDoctorApplicationRespons>>> GetApprovedDoctorsAsync(CancellationToken ct)
        {
            var empty = new List<ApprovedDoctorApplicationRespons>();
            return Task.FromResult(ResponseFactory.Success(empty, "No approved doctor applications available in this build."));
        }

        public Task<BaseResponse<string>> RejectDoctorAsync(Guid applicationId, string reason, CancellationToken ct)
        {
            return Task.FromResult(ResponseFactory.Fail<string>("RejectDoctor is not implemented in this build."));
        }

        public Task<BaseResponse<string>> ApplyForDoctorAsync(Guid ApplicationId, Guid DepartmentId, CancellationToken CT)
        {
            return Task.FromResult(ResponseFactory.Fail<string>("ApplyForDoctor is not implemented in this build."));
        }
    }
}
////using Application.Common.Interfaces.Admin;
////using Application.Common.Interfaces.Authentication;
////using Application.Common.Responses;
////using Application.DTOS.Responses;
////using Domain.Entites;
////using Domain.Entites.DoctorsModule;
////using Domain.Enums;
////using Infrastructure.Persistence.Identity;
////using Microsoft.AspNetCore.Identity;
////using Microsoft.EntityFrameworkCore;

//using Application.Common.Interfaces.Admin;
//using Application.Common.Interfaces.Authentication;
//using Application.Common.Responses;
//using Application.DTOS.Responses;
//using Domain.Entites;
//using Domain.Entites.DoctorsModule;
//using Domain.Enums;
//using Infrastructure.Persistence.Identity;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Infrastructure.services.Admin.DoctorManagement
//{
//    public class AdminDoctorManagementService : IAdminDoctorManagement
//    {
//        private readonly AddIdentityDbContext _Context;
//        private readonly IEmailService _emailService;
//        private readonly UserManager<AppUser> _userManager;

//        public AdminDoctorManagementService(AddIdentityDbContext context, IEmailService emailService,
//            UserManager<AppUser> userManager)
//        {
//            _Context = context;
//            _emailService = emailService;
//            _userManager = userManager;
//        }

//        #region GetApprovedDoctorsAsync
//        public async Task<BaseResponse<List<ApprovedDoctorApplicationRespons>>> GetApprovedDoctorsAsync(CancellationToken ct)
//        {
//            var approve = await _Context.DoctorApplications.Where(d => d.Status == Status.Approved)
//                .OrderByDescending(d => d.SubmittedAt)
//                .Select(a => new ApprovedDoctorApplicationRespons
//                {
//                    ApplicationId = a.Id,
//                    DoctorName = a.FullName,
//                    Email = a.Email,
//                    specialization = a.Specialization,
//                }).ToListAsync(ct);

//            if (approve == null || approve.Count == 0)
//            {
//                return ResponseFactory.Fail<List<ApprovedDoctorApplicationRespons>>("No approved doctors found.", new List<string> { "No Doctor approved available." });
//            }
//            return ResponseFactory.Success<List<ApprovedDoctorApplicationRespons>>(approve, "Approved doctors retrieved successfully.");
//        }
//        #endregion

//        #region GetPendingDoctorsAsync
//        public async Task<BaseResponse<List<PendingDoctorApplicationResponse>>> GetPendingDoctorsAsync(CancellationToken ct)
//        {
//            var pending = await _Context.DoctorApplications.Where(d => d.Status == Status.Pending)
//                 .OrderByDescending(d => d.SubmittedAt)
//                 .Select(p => new PendingDoctorApplicationResponse
//                 {
//                     ApplicationId = p.Id,
//                     DoctorName = p.FullName,
//                     Email = p.Email,
//                     PhoneNumber = p.PhoneNumber,
//                     specialization = p.Specialization,
//                     SubmittedAt = p.SubmittedAt
//                 }).ToListAsync(ct);

//            if (pending == null || pending.Count == 0)
//            {
//                return ResponseFactory.Fail<List<PendingDoctorApplicationResponse>>("No Doctor Pending", new List<string> { "No Doctor pending available." });
//            }

//            return ResponseFactory.Success<List<PendingDoctorApplicationResponse>>(pending, "Pending Doctor Application Retrieved successfully.");
//        }
//        #endregion

//        #region ApplyForDoctorAsync
//        public async Task<BaseResponse<string>> ApplyForDoctorAsync(Guid ApplicationId, Guid DepartmentId, CancellationToken ct)
//        {
//            var application = await _Context.DoctorApplications.FindAsync(ApplicationId);
//            if (application == null)
//                return ResponseFactory.Fail<string>("Application not found.");

//            if (application.Status == Status.Approved)
//                return ResponseFactory.Fail<string>("Already approved.");

//            application.Status = Status.Approved;
//            await _Context.SaveChangesAsync(ct);

//            var password = $"Doctor@{Guid.NewGuid().ToString("N").Substring(0, 8)}{application.FullName}";
//            var user = new AppUser
//            {
//                Email = application.Email,
//                UserName = application.UserName,
//                PhoneNumber = application.PhoneNumber,
//                FirstName = application.FullName.Split(' ')[0],
//                LastName = application.FullName.Split(' ').Last(),
//                RoleType = RoleType.Doctor,
//                IsActive = true
//            };

//            var createResult = await _userManager.CreateAsync(user, password);
//            if (!createResult.Succeeded)
//            {
//                if (createResult.Errors.Any(e => e.Code == "DuplicateUserName"))
//                {
//                    return ResponseFactory.Fail<string>("This username already exists.");
//                }
//                return ResponseFactory.Fail<string>("Failed to create user account for the doctor.");
//            }

//            await _userManager.AddToRoleAsync(user, "Doctor");

//            var profile = new DoctorProfile
//            {
//                UserId = user.Id,
//                FullName = application.FullName,
//                Email = application.Email,
//                PhoneNumber = application.PhoneNumber,
//                Gender = application.Gender,
//                Specialization = application.Specialization,
//                Qualifications = application.Qualifications,
//                Address = application.Address,
//                Degree = application.Degree,
//                LicenseNumber = application.LicenseNumber,
//                NationalId = application.NationalId,
//                ImagePath = application.ImagePath,
//                Experience = application.Experience,
//                CreatedAt = DateTime.UtcNow,
//                DepartmentId = DepartmentId
//            };

//            _Context.DoctorProfiles.Add(profile);
//            await _Context.SaveChangesAsync(ct);

//            await _emailService.SendAsync(application.Email, "Doctor Application Approved",
//                $"Congratulations! Your doctor application has been approved. You can now log in with the following credentials: Email: {application.Email} Password: {password}");

//            return ResponseFactory.Success("Doctor application approved and email sent to the applicant.");
//        }
//        #endregion

//        #region RejectDoctorAsync
//        //public async Task<BaseResponse<string>> RejectDoctorAsync(Guid applicationId, string reason, CancellationToken ct)
//        //{
//        //    var result = await _Context.DoctorApplications.FindAsync(applicationId);

//        //    if (result == null)
//        //        return ResponseFactory.Fail<string>("Application not found.");

//        //    if (result.Status == Status.Rejected)
//        //        return ResponseFactory.Fail<string>("Already rejected");

//        //    result.Status = Status.Rejected;
//        //    await _Context.SaveChangesAsync(ct);

//        //    await _emailService.SendAsync(result.Email, "Doctor Application Rejected", $"Sorry, your application has been rejected.\nReason: {reason}");

//        //    return ResponseFactory.Success("Application rejected.");
//        //}
//        #endregion
//    }
//}

////namespace Infrastructure.services.Admin.DoctorManagement
////{
////    public class AdminDoctorManagementService : IAdminDoctorManagement
////    {
////        private readonly AddIdentityDbContext _Context;
////        private readonly IEmailService _emailService;
////        private readonly UserManager<AppUser> _userManager;
////        public AdminDoctorManagementService(AddIdentityDbContext context, IEmailService emailService,
////            UserManager<AppUser> userManager)
////        {
////            _Context = context;
////            _emailService = emailService;
////            _userManager = userManager;
////        }

////        #region GetApprovedDoctorsAsync
////        public async Task<BaseResponse<List<ApprovedDoctorApplicationRespons>>> GetApprovedDoctorsAsync(CancellationToken ct)
////        {
////            var approve = await _Context.DoctorApplications.Where(d => d.Status == Status.Approved)
////                .OrderByDescending(d => d.SubmittedAt)
////                .Select(a=> new ApprovedDoctorApplicationRespons 
////                {
////                ApplicationId=a.Id,
////                DoctorName=a.FullName,
////                Email=a.Email,
////                specialization=a.Specialization,
////                }).ToListAsync(ct);

////            if (approve == null || approve.Count == 0)
////            {
////                return ResponseFactory.Fail<List<ApprovedDoctorApplicationRespons>>
////                    ("No approved doctors found.", new List<string> { "No Doctor approved available." });
////            }
////            return ResponseFactory.Success<List<ApprovedDoctorApplicationRespons>>(approve, "Approved doctors retrieved successfully.");

////        }
////        #endregion

////        #region GetPendingDoctorsAsync
////        public async Task<BaseResponse<List<PendingDoctorApplicationResponse>>> GetPendingDoctorsAsync(CancellationToken ct)
////        {
////            var pending = await _Context.DoctorApplications.Where(d => d.Status == Status.Pending)
////                 .OrderByDescending(d => d.SubmittedAt)
////                 .Select(p => new PendingDoctorApplicationResponse
////                 {
////                     ApplicationId = p.Id,
////                     DoctorName = p.FullName,
////                     Email = p.Email,
////                     PhoneNumber = p.PhoneNumber,
////                     specialization = p.Specialization,
////                     SubmittedAt = p.SubmittedAt

////                 }).ToListAsync(ct);
////            if (pending == null || pending.Count == 0)
////            {
////                return ResponseFactory.Fail<List<PendingDoctorApplicationResponse>>
////                   ("No Doctor Pending",
////                   new List<string> { "No Doctor pending available." });
////            }

////            return ResponseFactory.Success<List<PendingDoctorApplicationResponse>>
////                (pending, "Pending Doctor Application Retrieved  successfully.");

////        }
////        #endregion

////        #region ApplyForDoctorAsync
////        public async Task<BaseResponse<string>> ApplyForDoctorAsync(Guid ApplicationId, Guid DepartmentId, CancellationToken ct)
////        {
////            var application = await _Context.DoctorApplications.FindAsync(ApplicationId);
////            if (application == null)
////                return ResponseFactory.Fail<string>("Application not found.");


////            if (application.Status == Status.Approved)
////                return ResponseFactory.Fail<string>("Already approved.");


////            application.Status = Status.Approved;
////            await _Context.SaveChangesAsync(ct);
////            var password = $"Doctor@{Guid.NewGuid().ToString("N").Substring(0, 8)}{application.FullName}";
////            //new Random().Next(1000, 9999)
////            var user = new AppUser
////            {
////                Email = application.Email,
////                UserName = application.Email,
////                PhoneNumber = application.PhoneNumber,
////                FirstName = application.FullName.Split(" ")[0],
////                LastName = application.FullName.Split(" ").Last(),
////                RoleType = RoleType.Doctor,
////                IsActive = true
////            };
////            var createResult = await _userManager.CreateAsync(user, password);
////            if (!createResult.Succeeded)
////            {
////                if (createResult.Errors.Any(e => e.Code == "DuplicateUserName"))
////                {

////                    return ResponseFactory.Fail<string>("This username already exists.");
////                }
////                return ResponseFactory.Fail<string>("Failed to create user account for the doctor.");
////            }
////            await _userManager.AddToRoleAsync(user, "Doctor");

////            var profile = new DoctorProfile
////            {
////                UserId = user.Id,
////                FullName = application.FullName,
////                Email = application.Email,
////                PhoneNumber = application.PhoneNumber,
////                Gender = application.Gender,
////                Specialization = application.Specialization,
////                Qualifications = application.Qualifications,
////                Address = application.Address,
////                Degree = application.Degree,
////                LicenseNumber = application.LicenseNumber,
////                NationalId = application.NationalId,
////                ImagePath = application.ImagePath,
////                Experience = application.Experience,
////                CreatedAt = DateTime.UtcNow,
////                DepartmentId = DepartmentId
////            };
////            _Context.DoctorProfiles.Add(profile);
////            await _Context.SaveChangesAsync(ct);

////            await _emailService.SendAsync(application.Email, "Doctor Application Approved",
////                $"Congratulations! Your doctor application has been approved." +
////                $" You can now log in with the following credentials" +
////                $":\t\tEmail: {application.Email}\tPassword: {password}"

////                );
////            return ResponseFactory.Success("Doctor application approved and email sent to the applicant.");

////        }
////        #endregion

////        #region RejectDoctorAsync
////        public async Task<BaseResponse<string>> RejectDoctorAsync(Guid applicationId, string reason, CancellationToken ct)
////        {
////            var result = await _Context.DoctorApplications.FindAsync(applicationId);

////            if (result == null)
////                return ResponseFactory.Fail<string>("Application not found.");

////            if (result.Status == Status.Rejected)
////                return ResponseFactory.Fail<string>("Already rejected");


////            result.Status = Status.Rejected;
////            await _Context.SaveChangesAsync(ct);

////            await _emailService.SendAsync(
////            result.Email,
////            "Doctor Application Rejected",
////            $"Sorry, your application has been rejected.\nReason: {reason}");

////            return ResponseFactory.Success
////                ("Application rejected.");

////        }
////        #endregion
////    }
////}
