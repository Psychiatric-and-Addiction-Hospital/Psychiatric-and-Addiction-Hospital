using Domain.Entites.HR.Recruitment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations.HR.Recruitment
{
    public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
    {

        public void Configure(EntityTypeBuilder<Candidate> builder)
        {

            builder.Ignore(x => x.FullName);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(c => c.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.ResumeUrl)
                .HasMaxLength(1000);

            builder.Property(c => c.LinkedInUrl)
                .HasMaxLength(500);

            builder.Property(c => c.CurrentCompany)
                .HasMaxLength(200);

            builder.Property(c => c.CurrentPosition)
                .HasMaxLength(200);

            builder.Property(c => c.Notes)
                .HasMaxLength(2000);

            builder.Property(c => c.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(c => c.YearsOfExperience)
                .IsRequired()
                .HasDefaultValue(0);

            builder.HasIndex(c => c.Email)
                .IsUnique();

            builder.HasIndex(c => c.PhoneNumber);

            builder.HasMany(c => c.Applications)
                .WithOne(a => a.Candidate)
                .HasForeignKey(a => a.CandidateId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.AppUser)
                .WithOne(u => u.Candidate)
                .HasForeignKey<Candidate>(c => c.AppUserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.ToTable("Candidates", table =>
            {
                table.HasCheckConstraint(
                    "CK_Candidate_YearsOfExperience",
                    "[YearsOfExperience] >= 0");

                table.HasCheckConstraint(
                    "CK_Candidate_FirstName",
                    "LEN(LTRIM(RTRIM([FirstName]))) > 0");

                table.HasCheckConstraint(
                    "CK_Candidate_LastName",
                    "LEN(LTRIM(RTRIM([LastName]))) > 0");
            });
        }
    }
}

