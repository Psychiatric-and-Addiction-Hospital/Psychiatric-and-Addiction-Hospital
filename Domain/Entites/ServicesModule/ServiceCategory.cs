using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites.ServicesModule
{
    public class ServiceCategory
    {
        public Guid Id { get; set; }=Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public ICollection<Service> Services { get; set; } = new List<Service>();
    }
}
