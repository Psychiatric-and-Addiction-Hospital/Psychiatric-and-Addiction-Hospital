using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Doctor.ManagementDoctor
{
    public record GetAllDoctorsQuery() : IRequest<BaseResponse<List<DoctorProfileResponse>>>;
}
