using Application.Common.Interfaces.Report;
using Application.Common.Responses;
using Application.DTOS.Responses.Report;
using Infrastructure.Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.Report.ReportDoctor
{
    public  class CreateReportServies : ICreateReport
    {
        private readonly AddIdentityDbContext _Recontext;


        public CreateReportServies(AddIdentityDbContext recontext)
        {
            _Recontext = recontext;
        }
        public async Task<BaseResponse<ReportResponse>> CreateReportAsync(
        string patientId,
        Guid sessionId,
        string doctorId ,
        string diagnosis,
        string notes,
        string treatmentPlan,
        int conditionRate,
        string? attachmentUrl,
        DateTime createdAt , CancellationToken ct)
        {

            var report = new Domain.Entites.Report
            {
                DoctorId = doctorId,
                PatientId = patientId,
                SessionId = sessionId,
                Diagnosis = diagnosis,
                Notes = notes,
                TreatmentPlan = treatmentPlan,
                ConditionRate = conditionRate,
                AttachmentUrl = attachmentUrl,
                CreatedAt = createdAt
            };

            await _Recontext.Reports.AddAsync(report, ct);
            await _Recontext.SaveChangesAsync(ct);




            var response = new BaseResponse<ReportResponse>();
            try
            {
                
                response.Success = true;
                response.Message = "Report created successfully.";
                response.Data = new ReportResponse
                {
                };
            }
            catch (Exception ex)
            {
               
                response.Success = false;
                response.Message = "An error occurred while creating the report.";
                response.Errors.Add(ex.Message);
            }
            return response;
        }



    }
}
