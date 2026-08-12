using Domain.Entites.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.HR
{
    public class PayrollConfiguration : IEntityTypeConfiguration<Payroll>
    {
        public void Configure(EntityTypeBuilder<Payroll> builder)
        {
            builder.ToTable("Payrolls", table =>
            {
                table.HasCheckConstraint(
                    "CK_Payroll_Amount",
                    "[Amount] >= 0");
            });

            #region Properties

            builder.Property(p => p.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.EffectiveDate)
                .IsRequired();

            builder.Property(p => p.PayrollType)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(p => p.Status)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(1000);

            builder.Property(p => p.ReferenceNumber)
                .HasMaxLength(100);

            #endregion

            #region Indexes

            builder.HasIndex(p => p.EmployeeId);

            builder.HasIndex(p => p.EffectiveDate);

            builder.HasIndex(p => p.Status);

            builder.HasIndex(p => p.PayrollType);

            builder.HasIndex(p => p.ReferenceNumber)
                .IsUnique()
                .HasFilter("[ReferenceNumber] IS NOT NULL");

            #endregion

            #region Relationships

            builder.HasOne(p => p.Employee)
                .WithMany(e => e.Payrolls)
                .HasForeignKey(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion
        }
    }
}

