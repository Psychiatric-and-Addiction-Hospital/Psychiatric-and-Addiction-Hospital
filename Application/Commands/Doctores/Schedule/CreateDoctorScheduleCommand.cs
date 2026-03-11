using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;
using System;

namespace Application.Commands.Doctores.Schedule
{
    public record CreateDoctorScheduleCommand(Guid DoctorId, DateTime Date, string Time)
        : IRequest<BaseResponse<ScheduleResponse>>;


}
