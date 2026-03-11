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
    public class GetAllDoctorsHandler : IRequestHandler<GetAllDoctorsQuery, BaseResponse<List<DoctorProfileResponse>>>
    {
        private readonly IGetAllDoctors _getAllDoctors;
        public GetAllDoctorsHandler(IGetAllDoctors getAllDoctors)
        {
            _getAllDoctors= getAllDoctors;
        }   
        public async Task<BaseResponse<List<DoctorProfileResponse>>> Handle(GetAllDoctorsQuery request, CancellationToken ct)
        {
            return await _getAllDoctors.GetAllDoctorsAsync(ct);
        }
    }
}
