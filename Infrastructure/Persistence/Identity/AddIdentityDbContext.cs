using Domain.Entites;
using Domain.Entites.Authentication;
using Domain.Entites.BlogModule;
using Domain.Entites.ServicesModule;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Identity
{
    public class AddIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AddIdentityDbContext(DbContextOptions<AddIdentityDbContext> options) : base(options)
        { }

        public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;
        public DbSet<PasswordResetCode> PasswordResetCodes { get; set; }= default!;
        public DbSet<DoctorApplication> DoctorApplications { get; set; } = default!;
        public DbSet<DoctorProfile> DoctorProfiles { get; set; } = default!;
        public DbSet<PatientProfile> PatientProfiles { get; set; } = default!;
        public DbSet<Session> Sessions { get; set; } = default!;
        public DbSet<Supporter> Supporters { get; set; } = default!;
        public DbSet<ProgressTracker> ProgressTrackers { get; set; } = default!;
        public DbSet<Resource> Resources { get; set; } = default!;
        public DbSet<Notification> Notifications { get; set; } = default!;
        public DbSet<ChatMessage> ChatMessages { get; set; } = default!;
        #region Blog
        public DbSet<BlogPost> BlogPosts { get; set; } = default!;
        public DbSet<Tages> Tages { get; set; } = default!;
        public DbSet<Comments> Comments { get; set; } = default!;
        public DbSet<BlogCategory> BlogCategorys { get; set; } = default!;
        public DbSet<BlogPostTag> BlogPostTags { get; set; } = default!;

        #endregion

        #region Services Module
        public DbSet<Service> Services { get; set; } = default!;
        public DbSet<ServiceCategory> ServiceCategory { get; set; } = default!;
        #endregion


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(AddIdentityDbContext).Assembly);
        }
    }
}
