using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    // What Api returns after savigng a doctor application by => T1-Z3.
    public class DoctorApplicationResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Specialization { get; set; }
        public string Status { get; set; }
        public DateTime AppliedAt { get; set; }
    }
}
