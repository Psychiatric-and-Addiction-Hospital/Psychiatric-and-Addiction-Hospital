using Domain.Entites.HR.Recruitment;
using Domain.Enums.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Persistence.Configurations.HR.Recruitment
{
    public class ApplicationConfiguration : IEntityTypeConfiguration<Domain.Entites.HR.Recruitment.Application>
    {

        public void Configure(EntityTypeBuilder<Domain.Entites.HR.Recruitment.Application> builder)
        {
            #region Table

            builder.ToTable("Applications", table =>
            {
                table.HasCheckConstraint(
                    "CK_Application_AppliedDate",
                    "[AppliedDate] <= GETDATE()");
            });

            #endregion

            #region Properties

            builder.Property(a => a.AppliedDate)
                .IsRequired();

            builder.Property(x => x.Status)
        .HasConversion<string>()
        .HasMaxLength(30)
        .HasDefaultValue(ApplicationStatus.Pending)
        .IsRequired();

            builder.Property(a => a.Notes)
                .HasMaxLength(2000);

            builder.Property(x => x.CoverLetter)
    .HasMaxLength(3000);

            builder.Property(x => x.ResumeSnapshotUrl)
                .HasMaxLength(1000);

            #endregion

            #region Indexes

            builder.HasIndex(a => new
            {
                a.CandidateId,
                a.JobPostingId
            })
            .IsUnique();

            builder.HasIndex(a => a.Status);

            builder.HasIndex(a => a.AppliedDate);

            #endregion

            #region Relationships

            builder.HasOne(a => a.Candidate)
                .WithMany(c => c.Applications)
                .HasForeignKey(a => a.CandidateId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.JobPosting)
                .WithMany(j => j.Applications)
                .HasForeignKey(a => a.JobPostingId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.Interviews)
                .WithOne(i => i.Application)
                .HasForeignKey(i => i.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Offer)
                .WithOne(o => o.Application)
                .HasForeignKey<ApplicationOffer>(o => o.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
        }
    }
}

