using Application.Commands.Doctores;
using Application.Common.Interfaces.Doctores;
using FluentResults;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Doctores
{
    public class DoctorApplyHandler : IRequestHandler<DoctorApplyCommand, Result<string>>
    {
        private readonly IDoctoreApplication _doctore;
        public DoctorApplyHandler(IDoctoreApplication doctore)
        {
            _doctore = doctore;
        }
        public async Task<Result<string>> Handle(DoctorApplyCommand request, CancellationToken cancellationToken)
        {
           return await _doctore.ApplyForDoctorAsync(request.FullName, request.Email,
               request.PhoneNumber, request.Gender, request.Specialization,request.Qualifications,
               request.LicenseNumber,request.ClinicAddress,request.NationalId,request.Address,request.Degree, cancellationToken);
        }
    }
}
