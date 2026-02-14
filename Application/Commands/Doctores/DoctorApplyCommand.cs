using Domain.Enums;
using FluentResults;
using MediatR;
namespace Application.Commands.Doctores
{
    public record DoctorApplyCommand
    (
        string FullName,
        string Email,
        string PhoneNumber,
        Gender Gender,
        string Specialization,
        string Qualifications,
        string LicenseNumber,
    string ClinicAddress,
    string NationalId,
    string Address,
    string Degree)
        : IRequest<Result<string>>;
}
