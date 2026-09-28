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
    [Route("api/Patient/Reports")]
    public class PatientReportsController : BaseController
    {
        private readonly ISender _sender;
        public PatientReportsController(ISender sender) => _sender = sender;

        /// المريض يشوف كل تقاريره الطبية
        /// GET /api/Patient/Reports/GetAll
      
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetMyReports(CancellationToken ct)
        {
            var patientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(patientId))
                return Unauthorized();

            var result = await _sender.Send(new GetPatientReportsQuery(patientId), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// المريض يشوف تقرير واحد بالتفصيل
        /// GET /api/Patient/Reports/GetById/{id}
        /// </summary>
        [HttpGet("GetById/{id:guid}")]
        public async Task<IActionResult> GetReport(Guid id, CancellationToken ct)
        {
            var result = await _sender.Send(new GetReportByIdQuery(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        /// <summary>
        /// المريض يشوف كل تقارير جلسة معينة
        /// GET /api/Patient/Reports/GetSessionReports/{sessionId}
        /// </summary>
        [HttpGet("GetSessionReports/{sessionId:guid}")]
        public async Task<IActionResult> GetSessionReports(Guid sessionId, CancellationToken ct)
        {
            var result = await _sender.Send(new GetSessionReportsQuery(sessionId), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
