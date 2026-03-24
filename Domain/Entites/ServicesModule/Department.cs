using Domain.Common;
using Domain.Entites.DoctorsModule;
using System.Collections.Generic;
namespace Domain.Entites.ServicesModule
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<DoctorProfile> Doctors { get; set; } = new List<DoctorProfile>();
        public ICollection<Service> Services { get; set; } = new List<Service>();
    }
}
