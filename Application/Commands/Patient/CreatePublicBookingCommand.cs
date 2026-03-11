using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;
using System;

namespace Application.Commands.Patient
{
    public record CreatePublicBookingCommand
        (string FullName, string PhoneNumber,string Email, Guid DoctorId, Guid ScheduleId, string Notes) 
        : IRequest<BaseResponse<PublicBookingResponse>>;

     
    
}
