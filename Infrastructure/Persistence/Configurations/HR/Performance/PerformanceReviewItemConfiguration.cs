using Domain.Entites.HR.Performance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Persistence.Configurations.HR.Performance
{
    public class PerformanceReviewItemConfiguration : IEntityTypeConfiguration<PerformanceReviewItem>
    {
        public void Configure(EntityTypeBuilder<PerformanceReviewItem> builder)
        {

            builder.ToTable("PerformanceReviewItems", table =>
            {
                table.HasCheckConstraint(
                    "CK_PerformanceReviewItem_Score",
                    "[Score] >= 0 AND [Score] <= 100");
            });

            #region Properties

            builder.Property(i => i.Score)
                    .HasColumnType("decimal(5,2)")
                    .IsRequired();

            builder.Property(i => i.Comment)
                .HasMaxLength(1000);

            #endregion

            #region Indexes

            builder.HasIndex(i => i.PerformanceReviewId);

            builder.HasIndex(i => i.PerformanceCriteriaId);

            builder.HasIndex(i => new
            {
                i.PerformanceReviewId,
                i.PerformanceCriteriaId
            })
            .IsUnique();

            #endregion

            #region Relationships

            builder.HasOne(i => i.PerformanceReview)
                .WithMany(r => r.Items)
                .HasForeignKey(i => i.PerformanceReviewId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.PerformanceCriteria)
                .WithMany(c => c.ReviewItems)
                .HasForeignKey(i => i.PerformanceCriteriaId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion


        }
    }
}
