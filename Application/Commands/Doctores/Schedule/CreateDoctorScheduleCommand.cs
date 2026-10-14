using Application.Common.Responses;
using Application.DTOS.Request.Doctor;
using Application.DTOS.Responses;
using MediatR;

namespace Application.Commands.Doctores.Schedule
{
    public record CreateDoctorScheduleCommand(CreateDoctorRequest request) : IRequest<BaseResponse<ScheduleResponse>>;
}
