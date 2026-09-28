using Application.Commands.Patient;
using Application.Queries.Patient;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Psychiatric_and_Addiction_Hospital.Controllers.Patient
{
    [Authorize(Roles = "Patient")]
    [Route("api/Patient/MyAppointments")]
    public class MyAppointmentsController : BaseController
    {
        private readonly ISender _sender;
        public MyAppointmentsController(ISender sender) => _sender = sender;

        /// <summary>
        /// GET /api/Patient/MyAppointments/GetAll?status=All|Upcoming|Completed|Cancelled
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetMyAppointments(
            [FromQuery] string status = "All",
            CancellationToken ct = default)
        {
            var patientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(patientId)) return Unauthorized();

            var result = await _sender.Send(new GetMyAppointmentsQuery(patientId, status), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// GET /api/Patient/MyAppointments/GetDetails/{id}
        /// </summary>
        [HttpGet("GetDetails/{id:guid}")]
        public async Task<IActionResult> GetAppointmentDetails(Guid id, CancellationToken ct)
        {
            var patientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(patientId)) return Unauthorized();

            var result = await _sender.Send(new GetMyAppointmentDetailsQuery(id, patientId), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        /// <summary>
        /// POST /api/Patient/MyAppointments/Cancel/{id}
        /// </summary>
        [HttpPost("Cancel/{id:guid}")]
        public async Task<IActionResult> CancelAppointment(
            Guid id,
            [FromBody] string reason,
            CancellationToken ct)
        {
            var patientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(patientId)) return Unauthorized();

            var result = await _sender.Send(new CancelAppointmentCommand(id, patientId, reason), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// POST /api/Patient/MyAppointments/CancelRequest/{id}  ← إلغاء طلب Pending
        /// </summary>
        [HttpPost("CancelRequest/{id:guid}")]
        public async Task<IActionResult> CancelBookingRequest(Guid id, CancellationToken ct)
        {
            var patientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(patientId)) return Unauthorized();

            var result = await _sender.Send(new CancelBookingRequestCommand(id, patientId), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
