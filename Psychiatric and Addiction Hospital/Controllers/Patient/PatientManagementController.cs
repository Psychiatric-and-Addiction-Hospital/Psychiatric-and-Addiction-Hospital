using Application.Commands.Patient;
using Application.Queries.Patient;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Psychiatric_and_Addiction_Hospital.Controllers.Patient
{
    public class PatientManagementController : BaseController
    {
        private readonly ISender _sender;

        public PatientManagementController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("GetSessions/{patientId}")]
        public async Task<IActionResult> GetSessions(Guid patientId, CancellationToken ct)
        {
            var result = await _sender.Send(new GetPatientSessionsQuery(patientId), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

      
        [HttpGet("GetSessionDetails/{sessionId}")]
        public async Task<IActionResult> GetSessionDetails(Guid sessionId, CancellationToken ct)
        {
            var result = await _sender.Send(new GetSessionDetailsQuery(sessionId), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost("AddSessionNote")]
        public async Task<IActionResult> AddSessionNote([FromBody] AddSessionNoteCommand request, CancellationToken ct)
        {
            var result = await _sender.Send(request, ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("Dashboard/{patientId}")]
        public async Task<IActionResult> GetDashboard(string patientId, CancellationToken ct)
        {
            var result = await _sender.Send(new GetPatientDashboardQuery(patientId), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
