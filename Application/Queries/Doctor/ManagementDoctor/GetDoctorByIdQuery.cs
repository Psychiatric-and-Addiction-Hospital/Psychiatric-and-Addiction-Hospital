using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;
using System;

namespace Application.Queries.Doctor.ManagementDoctor
{
    public record GetDoctorByIdQuery(Guid Id): IRequest<BaseResponse<DoctorProfileResponse>>;

}
