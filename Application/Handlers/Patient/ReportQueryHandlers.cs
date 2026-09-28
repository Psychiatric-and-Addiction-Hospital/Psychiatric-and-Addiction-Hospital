using Application.Common.Interfaces.Patient;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Application.Queries.Patient;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Patient
{
    // Handler: GET all patient reports
    public class GetPatientReportsHandler : IRequestHandler<GetPatientReportsQuery, BaseResponse<List<SessionNoteResponse>>>
    {
        private readonly IReportService _service;
        public GetPatientReportsHandler(IReportService service) => _service = service;

        public async Task<BaseResponse<List<SessionNoteResponse>>> Handle(GetPatientReportsQuery request, CancellationToken ct)
            => await _service.GetPatientReportsAsync(request.PatientId, ct);
    }

    // Handler: GET single report by id
    public class GetReportByIdHandler : IRequestHandler<GetReportByIdQuery, BaseResponse<SessionNoteResponse>>
    {
        private readonly IReportService _service;
        public GetReportByIdHandler(IReportService service) => _service = service;

        public async Task<BaseResponse<SessionNoteResponse>> Handle(GetReportByIdQuery request, CancellationToken ct)
            => await _service.GetReportByIdAsync(request.ReportId, ct);
    }

    // Handler: GET all reports for a session
    public class GetSessionReportsHandler : IRequestHandler<GetSessionReportsQuery, BaseResponse<List<SessionNoteResponse>>>
    {
        private readonly IReportService _service;
        public GetSessionReportsHandler(IReportService service) => _service = service;

        public async Task<BaseResponse<List<SessionNoteResponse>>> Handle(GetSessionReportsQuery request, CancellationToken ct)
            => await _service.GetSessionReportsAsync(request.SessionId, ct);
    }
}
