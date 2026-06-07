using Application.Commands.Patient;
using Application.Queries.Patient;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Psychiatric_and_Addiction_Hospital.Controllers.Patient
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientManagementController : BaseController
    {
        private readonly ISender _sender;

        public PatientManagementController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Get all sessions for a patient (session timeline)
        /// </summary>
        [HttpGet("GetSessions/{patientId}")]
        public async Task<IActionResult> GetSessions(string patientId, CancellationToken ct)
        {
            var result = await _sender.Send(new GetPatientSessionsQuery(patientId), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Get full details of a single session including history of doctor notes
        /// </summary>
        [HttpGet("GetSessionDetails/{sessionId}")]
        public async Task<IActionResult> GetSessionDetails(Guid sessionId, CancellationToken ct)
        {
            var result = await _sender.Send(new GetSessionDetailsQuery(sessionId), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        /// <summary>
        /// Doctor adds a clinical note / report to a session (builds history timeline)
        /// </summary>
        [HttpPost("AddSessionNote")]
        public async Task<IActionResult> AddSessionNote([FromBody] AddSessionNoteCommand request, CancellationToken ct)
        {
            var result = await _sender.Send(request, ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Patient dashboard: next appointment + recent notes + recent sessions
        /// </summary>
        [HttpGet("Dashboard/{patientId}")]
        public async Task<IActionResult> GetDashboard(string patientId, CancellationToken ct)
        {
            var result = await _sender.Send(new GetPatientDashboardQuery(patientId), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
