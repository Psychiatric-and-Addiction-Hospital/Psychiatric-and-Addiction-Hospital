using Domain.Entites;
using Domain.Entites.Authentication;
using Domain.Entites.BlogModule;
using Domain.Entites.DoctorsModule;
using Domain.Entites.Features;

using Domain.Entites.HR;
using Domain.Entites.HR.Applications;

using Domain.Entites.ServicesModule;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence.Identity
{
    public class AddIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AddIdentityDbContext(DbContextOptions<AddIdentityDbContext> options)
            : base(options)
        {
        }
       
        public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;
        public DbSet<PasswordResetCode> PasswordResetCodes { get; set; } = default!;
        public DbSet<Report> Reports { get; set; } = default!;
        public DbSet<DoctorProfile> DoctorProfiles { get; set; } = default!;
        public DbSet<PatientProfile> PatientProfiles { get; set; } = default!;
        public DbSet<Session> Sessions { get; set; } = default!;
        public DbSet<Supporter> Supporters { get; set; } = default!;
        public DbSet<ProgressTracker> ProgressTrackers { get; set; } = default!;
        public DbSet<Resource> Resources { get; set; } = default!;
        public DbSet<Notification> Notifications { get; set; } = default!;
        public DbSet<ChatMessage> ChatMessages { get; set; } = default!;
        public DbSet<DoctorSchedule> DoctorSchedules { get; set; } = default!;
        public DbSet<Department> Departments { get; set; } = default!;
        public DbSet<PublicBooking> PublicBookings { get; set; } = default!;

        #region Blog
        public DbSet<BlogPost> BlogPosts { get; set; } = default!;
        public DbSet<Tages> Tages { get; set; } = default!;
        public DbSet<Comments> Comments { get; set; } = default!;
        public DbSet<BlogCategory> BlogCategorys { get; set; } = default!;
        public DbSet<BlogPostTag> BlogPostTags { get; set; } = default!;

        #endregion

        #region Services Module
        public DbSet<Service> Services { get; set; } = default!;
        #endregion


        #region
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Recruitment> Recruitments { get; set; }
      
        public DbSet<ApplicationProcess> ApplicationProcesses { get; set; }
        public DbSet<ApplicationInterview> ApplicationInterviews { get; set; }
        public DbSet<ApplicationOffer> ApplicationOffers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }

        #endregion
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); 

           
            builder.ApplyConfigurationsFromAssembly(typeof(AddIdentityDbContext).Assembly);
        }

    }
}



