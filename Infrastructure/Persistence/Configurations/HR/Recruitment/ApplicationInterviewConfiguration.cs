using Domain.Entites.HR.Recruitment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Persistence.Configurations.HR.Recruitment
{
    public class ApplicationInterviewConfiguration : IEntityTypeConfiguration<ApplicationInterview>
    {
        public void Configure(EntityTypeBuilder<ApplicationInterview> builder)
        {
            builder.ToTable("ApplicationInterviews", table =>
            {
                table.HasCheckConstraint(
                    "CK_ApplicationInterview_Score",
                    "[Score] >= 0 AND [Score] <= 100");
            });

            #region Properties

            builder.Property(i => i.ScheduledAt)
                .IsRequired();

            builder.Property(i => i.InterviewType)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(i => i.Location)
                .HasMaxLength(250);

            builder.Property(x => x.MeetingLink)
                .HasMaxLength(500);

            builder.Property(x => x.DurationInMinutes)
                .HasDefaultValue(60);

            builder.Property(i => i.Feedback)
                .HasMaxLength(2000);

            builder.Property(x => x.Result)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            #endregion

            #region Indexes

            builder.HasIndex(i => i.ApplicationId);

            builder.HasIndex(i => i.InterviewerId);

            builder.HasIndex(i => i.ScheduledAt);

            #endregion

            #region Relationships

            builder.HasOne(i => i.Application)
                .WithMany(a => a.Interviews)
                .HasForeignKey(i => i.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.Interviewer)
                .WithMany(e => e.InterviewsConducted)
                .HasForeignKey(i => i.InterviewerId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion
        }
    }
}


