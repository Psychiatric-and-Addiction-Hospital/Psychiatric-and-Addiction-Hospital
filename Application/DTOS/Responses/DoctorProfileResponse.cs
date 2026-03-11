using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Responses
{
    public class DoctorProfileResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Specialization { get; set; }
        public string Degree { get; set; }
        public string Experience { get; set; }
        public string ImagePath { get; set; }
        public Gender Gender { get; set; }
        public string DepartmentName { get; set; }
    }
}
