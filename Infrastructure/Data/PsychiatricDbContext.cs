using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class PsychiatricDbContext:DbContext
    {
        public PsychiatricDbContext(DbContextOptions<PsychiatricDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(PsychiatricDbContext).Assembly);
        }


        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<DoctorApplication>DoctorApplications { get; set; }
        public DbSet<DoctorProfile>DoctorProfiles { get; set; }
        public DbSet<Notification>Notifications { get; set; }
        public DbSet<PatientProfile>PatientProfiles { get; set; }
        public DbSet<ProgressTracker>ProgressTrackers { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Supporter> Supporters { get; set; }
        

    }
}
