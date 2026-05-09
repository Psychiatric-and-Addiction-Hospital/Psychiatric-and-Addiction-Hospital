using Application.Commands.Doctores.Booking;
using Application.DTOS.Responses.booking;
using Infrastructure.services.Doctores.Booking;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Psychiatric_and_Addiction_Hospital.Controllers.Doctor
{
    [Authorize(Roles = "Doctor,Admin")]
    public class BookingApprovalController : BaseController
    {
        private readonly ISender _sender;

        public BookingApprovalController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPut("approve/{bookingId}")]
        public async Task<IActionResult> ApproveBooking(Guid bookingId)
        {
            var result = await _sender.Send(new ApproveBookingCommand(bookingId));
            return result.Success ? Ok(result) : BadRequest(result);
        }

      
        [HttpPut("reject/{bookingId}")]
        public async Task<IActionResult> RejectBooking(Guid bookingId, [FromBody] RejectBookingResponse request)
        {
            var result = await _sender.Send(new RejectBookingCommand(bookingId, request.RejectionReason ));
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }

  
}
