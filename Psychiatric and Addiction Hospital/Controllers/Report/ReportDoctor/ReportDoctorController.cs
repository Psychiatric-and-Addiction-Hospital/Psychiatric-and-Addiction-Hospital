using Application.Commands.Doctores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers.Report.ReportDoctor
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportDoctorController : ControllerBase
    {
        private readonly ISender _sender;
        public ReportDoctorController(ISender sender)
        {
            _sender = sender;
        }
        [HttpPost("CreateReport")]
        public async Task<IActionResult> CreateReport([FromBody] CreateReportCommand request, CancellationToken ct)
        {
            var result = await _sender.Send(request, ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }   

    }
}
