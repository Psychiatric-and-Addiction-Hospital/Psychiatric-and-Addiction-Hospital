using Domain.Entites.BlogModule;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public RoleType RoleType { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; }
        public string Addres { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        public ICollection<ProgressTracker> ProgressTrackers { get; set; } = new List<ProgressTracker>();
        public ICollection<Resource> Resource { get; set; } = new List<Resource>();
        public ICollection<Session> DoctorSessions { get; set; } = new List<Session>();
        public ICollection<Session> PatientSessions { get; set; } = new List<Session>();
        public ICollection<Supporter> Supporter { get; set; } = new List<Supporter>();
        public ICollection<ChatMessage> SentMessages { get; set; } = new List<ChatMessage>();
        public ICollection<ChatMessage> ReceivedMessages { get; set; } = new List<ChatMessage>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public PatientProfile PatientProfile { get; set; }
        public DoctorApplication DoctorApplication { get; set; }
        public DoctorProfile DoctorProfile { get; set; }
        public ICollection<BlogPost> BlogPosts { get; set; }= new List<BlogPost>();
        public ICollection<Comments> Comments { get; set; }=new List<Comments>();


    }
}
