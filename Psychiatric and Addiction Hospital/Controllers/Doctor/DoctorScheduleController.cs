using Application.Commands.Doctores.Schedule;
using Application.Queries.Doctor.DoctorSchedule;
using Azure.Core;
using Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers.Doctor
{
    public class DoctorScheduleController : BaseController
    {
        private readonly ISender _sender;

        public DoctorScheduleController(ISender sender)
        {
            _sender=sender;
        }

        [HttpPost("CreateDoctorSchedule")]
        public async Task<IActionResult> CreateSchedule([FromBody] CreateDoctorScheduleCommand request)
        {
            var result = await _sender.Send(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetSchedules/{DoctorId}")]
        public async Task<IActionResult> GetSchedules(Guid DoctorId)
        {
            var result = await _sender.Send(new GetDoctorSchedulesQuery(DoctorId));
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("DeleteSchedules/{DoctorId}")]
        public async Task<IActionResult> DeleteSchedules(Guid DoctorId)
        {
            var result = await _sender.Send(new DeleteDoctorScheduleCommand(DoctorId));
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("available-appointments/{doctorId}")]
        public async Task<IActionResult> GetAvailableAppointments(Guid doctorId)
        {
            var result = await _sender.Send
               ( new GetDoctorAvailableAppointmentsQuery(doctorId));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("doctor/public-bookings{doctorId}")]
        public async Task<IActionResult> GetDoctorBookings(Guid doctorId)
        {
            var result = await _sender.Send(new GetDoctorPublicBookingsQuery(doctorId));
            return Ok(result);
        }
    }
}
