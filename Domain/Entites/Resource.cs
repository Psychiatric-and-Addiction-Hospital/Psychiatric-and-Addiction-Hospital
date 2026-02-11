using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Resource:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ResourceType ResourceType { get; set; }
        public string FileUrl { get; set; }
        public DateTime UploadedAt { get; set; } 
        public string UploadedById { get; set; }
        public AppUser UploadedBy { get; set; }
    }
}
