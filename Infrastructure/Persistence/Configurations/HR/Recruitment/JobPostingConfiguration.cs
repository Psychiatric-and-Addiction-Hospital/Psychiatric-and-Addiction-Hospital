using Domain.Entites.HR.Recruitment;
using Domain.Enums.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.HR.Recruitment
{
    public class JobPostingConfiguration : IEntityTypeConfiguration<JobPosting>
    {
        public void Configure(EntityTypeBuilder<JobPosting> builder)
        {
            #region Table

            builder.ToTable("JobPostings");

            #endregion

            #region Properties

            builder.Property(j => j.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(j => j.Description)
                .IsRequired()
                .HasMaxLength(4000);

            builder.Property(j => j.ExperienceLevel)
              .HasConversion<string>()
              .HasMaxLength(30)
              .IsRequired();

            builder.Property(x => x.Location)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(j => j.MinSalary)
                .HasColumnType("decimal(18,2)");

            builder.Property(j => j.MaxSalary)
                .HasColumnType("decimal(18,2)");

            builder.Property(j => j.EmploymentType)
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(x => x.WorkMode)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(x => x.Status)
       .HasConversion<string>()
       .HasMaxLength(20)
       .HasDefaultValue(JobPostingStatus.Draft)
       .IsRequired();

            builder.Property(x => x.Vacancies)
                .HasDefaultValue(1);

            builder.Property(j => j.PublishedDate)
                .IsRequired();

            builder.Property(j => j.ClosingDate)
                .IsRequired();

            #endregion

            #region Indexes

            builder.HasIndex(j => j.Title);

            builder.HasIndex(j => j.Status);

            builder.HasIndex(j => j.PublishedDate);

            builder.HasIndex(x => new
            {
                x.DepartmentId,
                x.PositionId
            });

            #endregion

            #region Relationships

            // Department

            builder.HasOne(j => j.Department)
                .WithMany(d => d.JobPostings)
                .HasForeignKey(j => j.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Position

            builder.HasOne(j => j.Position)
                .WithMany(p => p.JobPostings)
                .HasForeignKey(j => j.PositionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Hiring Manager

            builder.HasOne(j => j.HiringManager)
                .WithMany(e => e.ManagedJobPostings)
                .HasForeignKey(j => j.HiringManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Applications

            builder.HasMany(j => j.Applications)
                .WithOne(a => a.JobPosting)
                .HasForeignKey(a => a.JobPostingId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Constraints

            builder.HasCheckConstraint(
                "CK_JobPosting_Salary",
                "[MaxSalary] >= [MinSalary]");

            builder.HasCheckConstraint(
                "CK_JobPosting_MinSalary",
                "[MinSalary] >= 0");

            builder.HasCheckConstraint(
                "CK_JobPosting_ClosingDate",
                "[ClosingDate] >= [PublishedDate]");

            builder.HasCheckConstraint(
                "CK_JobPosting_Title_NotEmpty",
                "LEN(LTRIM(RTRIM([Title]))) > 0");

            builder.HasCheckConstraint("CK_JobPosting_Vacancies", "[Vacancies] > 0");

            builder.HasCheckConstraint(
    "CK_JobPosting_MaxSalary",
    "[MaxSalary] >= 0");

            #endregion
        }
    }
}

