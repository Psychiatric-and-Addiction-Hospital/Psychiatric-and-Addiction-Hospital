using Application.Common.Responses;
using Application.DTOS.Responses;
using Domain.Enums;
using MediatR;
using System;

namespace Application.Commands.Patient
{
    public record UpdatePatientProfileCommand(
        string UserId,
        string FullName,
        DateTime DateOfBirth,
        Gender Gender,
        MaritalStatus MaritalStatus,
        string Occupation,
        string Address,
        string PhoneNumber
    ) : IRequest<BaseResponse<PatientProfileResponse>>;
}
