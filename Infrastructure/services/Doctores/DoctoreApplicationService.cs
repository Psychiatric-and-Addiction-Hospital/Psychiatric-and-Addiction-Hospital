using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Doctores;
using Domain.Entites;
using Domain.Enums;
using FluentResults;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.services.Doctores
{
    public class DoctoreApplicationService : IDoctoreApplication
    {
        private readonly AddIdentityDbContext _Context;
        private readonly IEmailService _emailService;

        public DoctoreApplicationService(AddIdentityDbContext _context, IEmailService emailService)
        {
            _Context = _context;
            _emailService = emailService;
        }

        #region Apply For Doctor
        public async Task<Result<string>> ApplyForDoctorAsync(string FullName, string Email,
            string PhoneNumber, Gender Gender, string Specialization, string Qualifications,
            string LicenseNumber, string ClinicAddress,string NationalId, string Address,string Degree, CancellationToken CT)
        {
            var exists = await _Context.DoctorApplications.AnyAsync(ex => ex.Email == Email, CT);
            if (exists)
            {
                return Result.Fail("Application already submitted.");
            }
            #region Create Doctor Application

            var app = new DoctorApplication
            {
                FullName = FullName,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Gender=Gender,
                Specialization = Specialization,
                Qualifications = Qualifications,
                LicenseNumber = LicenseNumber,
                ClinicAddress= ClinicAddress,
                NationalId= NationalId,
                Address= Address,
                Degree= Degree,
                Status = Status.Pending,
                SubmittedAt = DateTime.UtcNow
            };

            #endregion

            await _Context.DoctorApplications.AddAsync(app, CT);
            await _Context.SaveChangesAsync(CT);
            return Result.Ok($"Application submitted successfully.\t {Status.Pending}");
        }



        #endregion

        



    }
}