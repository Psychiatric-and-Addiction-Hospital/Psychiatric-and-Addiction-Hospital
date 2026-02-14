using Domain.Entites;
using Domain.Enums;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Doctores
{
    public interface IDoctoreApplication
    {
        Task<Result<string>> ApplyForDoctorAsync(
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
            string Degree,
            CancellationToken CT);
    }
}
