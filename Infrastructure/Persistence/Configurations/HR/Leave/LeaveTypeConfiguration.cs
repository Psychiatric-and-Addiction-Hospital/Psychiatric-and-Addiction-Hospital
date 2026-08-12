using Domain.Entites.HR.Leave;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.HR.Leave
{
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {

            builder.ToTable("LeaveTypes", table =>
            {
                table.HasCheckConstraint(
                    "CK_LeaveType_MaxDaysPerYear",
                    "[MaxDaysPerYear] >= 0");
            });

            builder.Property(l => l.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(l => l.Description)
                .HasMaxLength(500);

            builder.Property(l => l.IsPaid)
                .HasDefaultValue(true);

            builder.Property(l => l.IsActive)
                .HasDefaultValue(true);

            builder.Property(l => l.RequiresApproval)
               .HasDefaultValue(true);

            builder.Property(l => l.MaxDaysPerYear)
                .HasDefaultValue(0);

            builder.HasIndex(l => l.Name)
                .IsUnique();

            builder.HasIndex(x => x.IsActive);

            builder.HasMany(l => l.LeaveRequests)
                .WithOne(r => r.LeaveType)
                .HasForeignKey(r => r.LeaveTypeId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
