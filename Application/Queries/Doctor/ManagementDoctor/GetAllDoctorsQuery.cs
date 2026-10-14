using Application.Common.Responses;
using Application.DTOS.Request.Doctor;
using Application.DTOS.Responses;
using MediatR;

namespace Application.Queries.Doctor.ManagementDoctor
{
    public record GetAllDoctorsQuery(DoctorListRequest request) : IRequest<BaseResponse<PagedResponse<DoctorProfileResponse>>>;
}
