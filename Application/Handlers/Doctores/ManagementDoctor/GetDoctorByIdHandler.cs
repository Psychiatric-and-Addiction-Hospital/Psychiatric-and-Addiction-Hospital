using Application.Common.Interfaces.Doctores.ManagementDoctor;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Application.Queries.Doctor.ManagementDoctor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Doctores.ManagementDoctor
{
    public class GetDoctorByIdHandler : IRequestHandler<GetDoctorByIdQuery, BaseResponse<DoctorProfileResponse>>
    {
        private readonly IGetDoctorById _getDoctorById;
        public GetDoctorByIdHandler(IGetDoctorById getDoctorById)
        {
            _getDoctorById = getDoctorById;
        }

        public async Task<BaseResponse<DoctorProfileResponse>> Handle(GetDoctorByIdQuery request, CancellationToken ct)
        {
            return await _getDoctorById.GetDoctorByIdAsync(request.Id, ct);
        }
    }
}
