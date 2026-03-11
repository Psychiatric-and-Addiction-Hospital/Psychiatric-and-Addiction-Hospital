using Domain.Entites;
using Domain.Entites.Authentication;
using Domain.Entites.BlogModule;
using Domain.Entites.DoctorsModule;
using Domain.Entites.Features;
using Domain.Entites.ServicesModule;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence.Identity
{
    public class AddIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AddIdentityDbContext(DbContextOptions<AddIdentityDbContext> options) : base(options)
        { }

        public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;
        public DbSet<PasswordResetCode> PasswordResetCodes { get; set; } = default!;
        public DbSet<Report> Reports { get; set; } = default!;

        public DbSet<DoctorApplication> DoctorApplications { get; set; } = default!;
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


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var dept1 = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var dept2 = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var dept3 = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var dept4 = Guid.Parse("44444444-4444-4444-4444-444444444444");

            builder.Entity<Department>().HasData(
                new Department { Id = dept1, Name = "Psychiatry" },
                new Department { Id = dept2, Name = "Addiction Treatment" },
                new Department { Id = dept3, Name = "Family Counseling" },
                new Department { Id = dept4, Name = "Child Psychiatry" }
            );

            builder.Entity<Service>().HasData(
       new Service
       {
           Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
           Name = "Depression Therapy",
           Description = "Treatment for depression",
           DepartmentId = dept1
       },
       new Service
       {
           Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
           Name = "Anxiety Treatment",
           Description = "Treatment for anxiety",
           DepartmentId = dept1
       },
       new Service
       {
           Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
           Name = "Detox Program",
           Description = "Detoxification for addiction",
           DepartmentId = dept2
       },
       new Service
       {
           Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
           Name = "Rehabilitation",
           Description = "Recovery program",
           DepartmentId = dept2
       }
   );

            builder.ApplyConfigurationsFromAssembly(typeof(AddIdentityDbContext).Assembly);


        }
    }
}
