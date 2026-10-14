using Application.Commands.Doctores.Report;
using Application.Common.Interfaces.Patient;
using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Doctores
{
    // Handler: PUT /api/DoctorReports/{id}  ← الطبيب يعدل تقرير
    public class UpdateReportHandler : IRequestHandler<UpdateReportCommand, BaseResponse<SessionNoteResponse>>
    {
        private readonly IReportService _service;
        public UpdateReportHandler(IReportService service) => _service = service;

        public async Task<BaseResponse<SessionNoteResponse>> Handle(UpdateReportCommand request, CancellationToken ct)
            => await _service.UpdateReportAsync(
                request.ReportId,
                request.DoctorId,
                request.Diagnosis,
                request.Notes,
                request.TreatmentPlan,
                request.ConditionRate,
                request.AttachmentUrl,
                ct);
    }
}
