using Domain.Common;
using Domain.Enums;
using System;


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
