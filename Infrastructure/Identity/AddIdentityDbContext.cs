using Domain.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class AddIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AddIdentityDbContext(DbContextOptions<AddIdentityDbContext> options) : base(options)
        { }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<PasswordResetCode> PasswordResetCodes { get; set; }
        public DbSet<DoctorApplication> DoctorApplications { get; set; }
        public DbSet<DoctorProfile> DoctorProfiles { get; set; }
        public DbSet<PatientProfile> PatientProfiles { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Supporter> Supporters { get; set; }
        public DbSet<ProgressTracker> ProgressTrackers { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(AddIdentityDbContext).Assembly);
        }
    }
}
