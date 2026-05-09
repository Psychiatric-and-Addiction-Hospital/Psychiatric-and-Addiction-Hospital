using Application.Common.Responses;
using Application.DTOS.Responses;
using Application.DTOS.Responses.booking;
using MediatR;
using System;

namespace Application.Commands.Doctores.Booking
{
    public record ApproveBookingCommand(Guid BookingId) 
        : IRequest<BaseResponse<ApproveBookingResponse>>;
}
