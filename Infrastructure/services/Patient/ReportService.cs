using Application.Common.Interfaces.Patient;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.services.Patient
{
    public class ReportService : IReportService
    {
        private readonly AddIdentityDbContext _context;

        public ReportService(AddIdentityDbContext context)
        {
            _context = context;
        }

        // ──────────────────────────────────────────────
        // GET all reports for a patient
        // ──────────────────────────────────────────────
        public async Task<BaseResponse<List<SessionNoteResponse>>> GetPatientReportsAsync(string patientId, CancellationToken ct)
        {
            var reports = await _context.Reports
                .Include(r => r.Doctor)
                .Include(r => r.Session)
                .Where(r => r.PatientId == patientId)
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new SessionNoteResponse
                {
                    Id            = r.Id,
                    SessionId     = r.SessionId,
                    DoctorId      = r.DoctorId,
                    DoctorName    = r.Doctor != null ? $"{r.Doctor.FirstName} {r.Doctor.LastName}" : "Unknown",
                    Diagnosis     = r.Diagnosis,
                    Notes         = r.Notes,
                    TreatmentPlan = r.TreatmentPlan,
                    ConditionRate = r.ConditionRate,
                    AttachmentUrl = r.AttachmentUrl,
                    CreatedAt     = r.CreatedAt
                })
                .ToListAsync(ct);

            return ResponseFactory.Success(reports, $"{reports.Count} report(s) retrieved");
        }

        // ──────────────────────────────────────────────
        // GET single report by ID
        // ──────────────────────────────────────────────
        public async Task<BaseResponse<SessionNoteResponse>> GetReportByIdAsync(Guid reportId, CancellationToken ct)
        {
            var r = await _context.Reports
                .Include(r => r.Doctor)
                .FirstOrDefaultAsync(r => r.Id == reportId, ct);

            if (r == null)
                return ResponseFactory.Fail<SessionNoteResponse>("Report not found");

            return ResponseFactory.Success(new SessionNoteResponse
            {
                Id            = r.Id,
                SessionId     = r.SessionId,
                DoctorId      = r.DoctorId,
                DoctorName    = r.Doctor != null ? $"{r.Doctor.FirstName} {r.Doctor.LastName}" : "Unknown",
                Diagnosis     = r.Diagnosis,
                Notes         = r.Notes,
                TreatmentPlan = r.TreatmentPlan,
                ConditionRate = r.ConditionRate,
                AttachmentUrl = r.AttachmentUrl,
                CreatedAt     = r.CreatedAt
            }, "Report retrieved successfully");
        }

        // ──────────────────────────────────────────────
        // GET all reports for a session
        // ──────────────────────────────────────────────
        public async Task<BaseResponse<List<SessionNoteResponse>>> GetSessionReportsAsync(Guid sessionId, CancellationToken ct)
        {
            var sessionExists = await _context.Sessions.AnyAsync(s => s.Id == sessionId, ct);
            if (!sessionExists)
                return ResponseFactory.Fail<List<SessionNoteResponse>>("Session not found");

            var reports = await _context.Reports
                .Include(r => r.Doctor)
                .Where(r => r.SessionId == sessionId)
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new SessionNoteResponse
                {
                    Id            = r.Id,
                    SessionId     = r.SessionId,
                    DoctorId      = r.DoctorId,
                    DoctorName    = r.Doctor != null ? $"{r.Doctor.FirstName} {r.Doctor.LastName}" : "Unknown",
                    Diagnosis     = r.Diagnosis,
                    Notes         = r.Notes,
                    TreatmentPlan = r.TreatmentPlan,
                    ConditionRate = r.ConditionRate,
                    AttachmentUrl = r.AttachmentUrl,
                    CreatedAt     = r.CreatedAt
                })
                .ToListAsync(ct);

            return ResponseFactory.Success(reports, $"{reports.Count} report(s) retrieved for session");
        }

        // ──────────────────────────────────────────────
        // UPDATE report (doctor only)
        // ──────────────────────────────────────────────
        public async Task<BaseResponse<SessionNoteResponse>> UpdateReportAsync(
            Guid reportId, string doctorId, string diagnosis, string notes,
            string treatmentPlan, int conditionRate, string? attachmentUrl,
            CancellationToken ct)
        {
            var report = await _context.Reports
                .Include(r => r.Doctor)
                .FirstOrDefaultAsync(r => r.Id == reportId, ct);

            if (report == null)
                return ResponseFactory.Fail<SessionNoteResponse>("Report not found");

            if (report.DoctorId != doctorId)
                return ResponseFactory.Fail<SessionNoteResponse>("You are not authorized to edit this report");

            report.Diagnosis     = diagnosis;
            report.Notes         = notes;
            report.TreatmentPlan = treatmentPlan;
            report.ConditionRate = conditionRate;
            report.AttachmentUrl = attachmentUrl;

            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(new SessionNoteResponse
            {
                Id            = report.Id,
                SessionId     = report.SessionId,
                DoctorId      = report.DoctorId,
                DoctorName    = report.Doctor != null ? $"{report.Doctor.FirstName} {report.Doctor.LastName}" : "Unknown",
                Diagnosis     = report.Diagnosis,
                Notes         = report.Notes,
                TreatmentPlan = report.TreatmentPlan,
                ConditionRate = report.ConditionRate,
                AttachmentUrl = report.AttachmentUrl,
                CreatedAt     = report.CreatedAt
            }, "Report updated successfully");
        }
    }
}
