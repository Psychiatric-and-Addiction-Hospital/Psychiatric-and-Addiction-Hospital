using Application.Common.Responses;
using Application.DTOS.Responses;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
namespace Application.Commands.Doctores
{
    public record DoctorApplyCommand
    (
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
    string Degree)
        : IRequest<BaseResponse<DoctorApplicationResponse>>;
}
