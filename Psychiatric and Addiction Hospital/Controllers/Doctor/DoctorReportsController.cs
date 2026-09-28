using Application.Commands.Patient;
using Application.Commands.Doctores.Report;
using Application.Queries.Patient;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Psychiatric_and_Addiction_Hospital.Controllers.Doctor
{
    [Authorize(Roles = "Doctor")]
    [Route("api/Doctor/Reports")]
    public class DoctorReportsController : BaseController
    {
        private readonly ISender _sender;
        public DoctorReportsController(ISender sender) => _sender = sender;

        /// <summary>
        /// الطبيب يكتب تقرير جديد لجلسة
        /// POST /api/Doctor/Reports/Create
        /// </summary>
        [HttpPost("Create")]
        public async Task<IActionResult> CreateReport([FromBody] AddSessionNoteCommand command, CancellationToken ct)
        {
            var result = await _sender.Send(command, ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// الطبيب يعدل تقرير موجود (يملكه هو فقط)
        /// PUT /api/Doctor/Reports/Update/{id}
        /// </summary>
        [HttpPut("Update/{id:guid}")]
        public async Task<IActionResult> UpdateReport(Guid id, [FromBody] UpdateReportCommand command, CancellationToken ct)
        {
            var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // تأكد أن الـ id في المسار هو نفسه في الـ body
            if (id != command.ReportId)
                return BadRequest("Report ID mismatch");

            // نمرر doctorId من JWT بدل ما يجي من الـ body لحماية أفضل
            var safeCommand = command with { DoctorId = doctorId };
            var result = await _sender.Send(safeCommand, ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// الطبيب يشوف كل تقارير جلسة معينة
        /// GET /api/Doctor/Reports/GetSessionReports/{sessionId}
        /// </summary>
        [HttpGet("GetSessionReports/{sessionId:guid}")]
        public async Task<IActionResult> GetSessionReports(Guid sessionId, CancellationToken ct)
        {
            var result = await _sender.Send(new GetSessionReportsQuery(sessionId), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
