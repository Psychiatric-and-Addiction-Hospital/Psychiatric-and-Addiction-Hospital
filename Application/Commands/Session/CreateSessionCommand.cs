using Application.Common.Responses;
using Domain.Enums;
using MediatR;
using System;

namespace Application.Commands.Session
{
    public record CreateSessionCommand(
    string DoctorId,
    string PatientId,
    DateOnly ScheduledDate,
    int DurationMinutes,
    SessionType SessionType) : IRequest<BaseResponse<Guid>>;
}
