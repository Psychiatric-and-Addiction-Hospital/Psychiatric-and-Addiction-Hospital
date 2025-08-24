using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SpecializationService : ISpecializationService
    {

        private readonly HashSet<string> _allowedSpecializations = new HashSet<string>
        {
             "Cardiology",          // أمراض القلب
             "Dermatology",         // الجلدية
             "Neurology",           // المخ والأعصاب
             "Pediatrics",          // طب الأطفال
             "Orthopedics",         // العظام
             "General Surgery",     // الجراحة العامة
             "Ophthalmology",       // طب العيون
             "Psychiatry",          // الطب النفسي
             "ENT",                 // أنف وأذن وحنجرة
             "Urology",             // المسالك البولية
             "Nephrology",          // أمراض الكلى
             "Gastroenterology",    // الجهاز الهضمي
             "Endocrinology",       // الغدد الصماء
             "Hematology",          // أمراض الدم
             "Oncology",            // الأورام
             "Pulmonology",         // الأمراض الصدرية (الرئة)
             "Rheumatology",        // الروماتيزم والمناعة
             "Infectious Disease",  // الأمراض المعدية
             "Allergy and Immunology", // الحساسية والمناعة
             "Plastic Surgery",     // جراحة التجميل
             "Vascular Surgery",    // جراحة الأوعية الدموية
             "Thoracic Surgery",    // جراحة الصدر
             "Neurosurgery",        // جراحة المخ والأعصاب
             "Obstetrics and Gynecology", // النساء والتوليد
             "Family Medicine",     // طب الأسرة
             "Emergency Medicine",  // طب الطوارئ
             "Anesthesiology",      // التخدير
             "Radiology",           // الأشعة التشخيصية
             "Pathology",           // الباثولوجيا
             "Geriatrics",          // طب الشيخوخة
             "Sports Medicine",     // طب الرياضة
             "Occupational Medicine", // الطب المهني
             "Public Health",       // الصحة العامة
             "Critical Care",       // العناية المركزة
             "Medical Genetics",    // الوراثة الطبية
             "Nuclear Medicine",    // الطب النووي
             "Forensic Medicine",   // الطب الشرعي
             "Dentistry",           // طب الأسنان
             "Oral and Maxillofacial Surgery" // جراحة الفم والفك
        };

        public bool IsValidSpecialization(string specialization)
        {
            if (string.IsNullOrWhiteSpace(specialization))
                return false;

            return _allowedSpecializations
                .Any(s => s.Equals(specialization, StringComparison.OrdinalIgnoreCase));
        }
    }
}
