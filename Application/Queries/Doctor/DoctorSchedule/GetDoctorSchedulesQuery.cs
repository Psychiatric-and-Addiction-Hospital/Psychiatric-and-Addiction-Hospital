using Application.Common.Responses;
using Application.DTOS.Request.Doctor;
using Application.DTOS.Responses;
using MediatR;
using System.Collections.Generic;


namespace Application.Queries.Doctor.DoctorSchedule
{
    public record GetDoctorSchedulesQuery(GetDoctorScheduleListRequest request) : IRequest<BaseResponse<PagedResponse<ScheduleResponse>>>;

}
