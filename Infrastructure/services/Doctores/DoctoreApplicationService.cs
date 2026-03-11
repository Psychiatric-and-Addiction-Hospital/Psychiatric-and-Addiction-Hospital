using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Doctores;
using Application.Common.Interfaces.UpLoad;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Domain.Entites.DoctorsModule;
using Domain.Enums;
using Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.services.Doctores
{
    public class DoctoreApplicationService : IDoctoreApplication
    {
        private readonly AddIdentityDbContext _Context;
        private readonly IFileStorage _fileStorage;


        public DoctoreApplicationService(AddIdentityDbContext _context, IFileStorage fileStorage)
        {
            _Context = _context;
            _fileStorage = fileStorage;
        }
        #region Apply For Doctor
        public async Task<BaseResponse<DoctorApplicationResponse>> ApplyForDoctorAsync(
           string FullName, string Email,
            string PhoneNumber, Gender Gender, IFormFile? ProfileImage,
            string Specialization, string Qualifications, string LicenseNumber, string Experience,
            string NationalId, string Address, string Degree, CancellationToken CT
            )
        {
            var exists = await _Context.DoctorApplications.AnyAsync(ex => ex.Email == Email, CT);
            if (exists)
            {
                return ResponseFactory.Fail<DoctorApplicationResponse>("Application already submitted.");
            }
            var imagePath = await _fileStorage.SaveDoctorImageAsync(ProfileImage, CT);
            #region Create Doctor Application

            var app = new DoctorApplication
            {
                FullName = FullName,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Gender = Gender,
                Specialization = Specialization,
                Qualifications = Qualifications,
                LicenseNumber = LicenseNumber,
                Experience = Experience,
                NationalId = NationalId,
                Address = Address,
                Degree = Degree,
                ImagePath = imagePath,
                Status = Status.Pending,
                SubmittedAt = DateTime.UtcNow
            };

            #endregion

            await _Context.DoctorApplications.AddAsync(app, CT);
            await _Context.SaveChangesAsync(CT);
            return ResponseFactory.Success(
                new DoctorApplicationResponse {
                    ApplicationId = app.Id,
                    FullName = app.FullName,
                    Status = app.Status
                },
                "Application submitted successfully.");

        }



        #endregion



    }
}