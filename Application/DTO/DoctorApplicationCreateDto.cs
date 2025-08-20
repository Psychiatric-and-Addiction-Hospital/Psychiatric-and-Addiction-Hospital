using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    // What customers will use to create a doctor application by => T1-Z3.
    public class DoctorApplicationCreateDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Specialization { get; set; }
        public string PhoneNumber { get; set; }
        public string CVFilePath { get; set; }
        public string Password { get; set; }

    }
}
