using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;
using System;


namespace Application.Commands.Doctores.Schedule
{
    public record DeleteDoctorScheduleCommand(Guid Id) : IRequest<BaseResponse<ScheduleResponse>>;
  
}
