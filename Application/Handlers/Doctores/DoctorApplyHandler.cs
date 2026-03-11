using Application.Commands.Doctores;
using Application.Common.Interfaces.Doctores;
using Application.Common.Responses;
using Application.DTOS.Responses;
using FluentResults;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Doctores
{
    public class DoctorApplyHandler : IRequestHandler<DoctorApplyCommand, BaseResponse<DoctorApplicationResponse>>
    {
        private readonly IDoctoreApplication _doctore;
        public DoctorApplyHandler(IDoctoreApplication doctore)
        {
            _doctore = doctore;
        }
        public async Task<BaseResponse<DoctorApplicationResponse>> Handle(DoctorApplyCommand request, CancellationToken ct)
        {
           return await _doctore.ApplyForDoctorAsync(request.FullName,request.Email, 
               request.PhoneNumber, request.Gender,request.ProfileImage,
               request.Specialization,request.Qualifications,request.LicenseNumber,request.Experience,
               request.NationalId,request.Address,request.Degree,ct
               );
        }
    }
}
