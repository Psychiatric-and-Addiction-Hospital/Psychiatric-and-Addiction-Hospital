using Domain.Entites.HR.Performance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.HR.Performance
{
    public class PerformanceReviewConfiguration : IEntityTypeConfiguration<PerformanceReview>
    {
        public void Configure(EntityTypeBuilder<PerformanceReview> builder)
        {
            builder.ToTable("PerformanceReviews", table =>
            {
                table.HasCheckConstraint(
                    "CK_PerformanceReview_OverallScore",
                    "[OverallScore] >= 0 AND [OverallScore] <= 100");
            });

            #region Properties

            builder.Property(r => r.ReviewDate)
                .IsRequired();

            builder.Property(r => r.Status)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(r => r.OverallScore)
                .HasColumnType("decimal(5,2)")
                .IsRequired();

            builder.Property(r => r.GeneralComment)
                .HasMaxLength(2000);

            #endregion

            #region Indexes

            builder.HasIndex(r => r.EmployeeId);

            builder.HasIndex(r => r.ReviewerId);

            builder.HasIndex(r => r.ReviewDate);

            builder.HasIndex(r => r.Status);

            builder.HasIndex(r => new
            {
                r.EmployeeId,
                r.ReviewDate
            });

            #endregion

            #region Relationships

            // Employee being reviewed
            builder.HasOne(r => r.Employee)
                .WithMany(e => e.PerformanceReviews)
                .HasForeignKey(r => r.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Reviewer (Manager / HR)
            builder.HasOne(r => r.Reviewer)
                .WithMany(e => e.ReviewsGiven)
                .HasForeignKey(r => r.ReviewerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Review Items
            builder.HasMany(r => r.Items)
                .WithOne(i => i.PerformanceReview)
                .HasForeignKey(i => i.PerformanceReviewId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
        }
    }
}

