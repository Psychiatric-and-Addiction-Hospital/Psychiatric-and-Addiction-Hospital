using Domain.Entites.HR.Performance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations.HR.Performance
{
    public class PerformanceCriteriaConfiguration : IEntityTypeConfiguration<PerformanceCriteria>
    {
        public void Configure(EntityTypeBuilder<PerformanceCriteria> builder)
        {
            builder.ToTable("PerformanceCriteria", table =>
            {
                table.HasCheckConstraint(
                    "CK_PerformanceCriteria_MaxScore",
                    "[MaxScore] > 0");
            });

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.Description)
                .HasMaxLength(1000);

            builder.Property(c => c.MaxScore)
                .HasDefaultValue(100);

            builder.Property(c => c.IsActive)
                .HasDefaultValue(true);

            builder.HasIndex(c => c.Name)
                .IsUnique();

            builder.HasMany(c => c.ReviewItems)
                .WithOne(i => i.PerformanceCriteria)
                .HasForeignKey(i => i.PerformanceCriteriaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
