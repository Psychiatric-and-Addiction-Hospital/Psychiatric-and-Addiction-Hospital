using System;
using System.Collections.Generic;

namespace Application.DTOS.Responses
{
    public class SessionNoteResponse
    {
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Diagnosis { get; set; }
        public string Notes { get; set; }
        public string TreatmentPlan { get; set; }
        public int ConditionRate { get; set; }
        public string? AttachmentUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
