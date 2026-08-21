using Application.Commands.Doctores.Schedule;
using Application.DTOS.Request.Doctor;
using Application.Queries.Doctor.DoctorSchedule;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers.Doctor
{
    public class DoctorScheduleController : BaseController
    {
        private readonly ISender _sender;

        public DoctorScheduleController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("CreateDoctorSchedule")]
        public async Task<IActionResult> CreateSchedule([FromBody] CreateDoctorRequest request)
        {
            var result = await _sender.Send(new CreateDoctorScheduleCommand(request));
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetSchedules")]
        public async Task<IActionResult> GetSchedules([FromQuery] GetDoctorScheduleListRequest request)
        {
            var result = await _sender.Send(new GetDoctorSchedulesQuery(request));
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("DeleteSchedules/{DoctorId}")]
        public async Task<IActionResult> DeleteSchedules(Guid DoctorId)
        {
            var result = await _sender.Send(new DeleteDoctorScheduleCommand(DoctorId));
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("available-appointments/{doctorId:guid}")]
        public async Task<IActionResult> GetAvailableAppointments(Guid doctorId)
        {
            var result = await _sender.Send
               (new GetDoctorAvailableAppointmentsQuery(doctorId));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("doctor/public-bookings")]
        public async Task<IActionResult> GetDoctorBookings()
        {
            var result = await _sender.Send(new GetDoctorPublicBookingsQuery());
            return Ok(result);
        }
    }
}
