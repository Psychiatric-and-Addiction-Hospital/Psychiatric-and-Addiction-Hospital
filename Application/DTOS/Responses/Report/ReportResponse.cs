using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Responses.Report
{
   public class ReportResponse
    {

        public string DoctorId;    
  
       public  string PatientId; 
       public  Guid SessionId;      
    public  string Diagnosis;     
    public  string Notes;         
    public  string TreatmentPlan;  
    public      int ConditionRate;  
    public  string? AttachmentUrl;
        public DateTime CreatedAt;


    }
}
