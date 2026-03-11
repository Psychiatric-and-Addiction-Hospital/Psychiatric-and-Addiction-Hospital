using Application.Common.Responses;
using Application.DTOS.Responses;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Doctores
{
    public interface IDoctoreApplication
    {
        Task<BaseResponse<DoctorApplicationResponse>> ApplyForDoctorAsync(
            string FullName,
        string Email,
        string PhoneNumber,
        Gender Gender,
         IFormFile? ProfileImage,
        string Specialization,
        string Qualifications,
        string LicenseNumber,
        string Experience,
    string NationalId,
    string Address,
    string Degree,
            CancellationToken CT);

    }
}
