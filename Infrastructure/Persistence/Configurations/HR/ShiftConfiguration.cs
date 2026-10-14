using Domain.Entites.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Persistence.Configurations.HR
{
    public class ShiftConfiguration : IEntityTypeConfiguration<Shift>
    {
        public void Configure(EntityTypeBuilder<Shift> builder)
        {
            #region Table

            builder.ToTable("Shifts", table =>
            {
                table.HasCheckConstraint(
                    "CK_Shift_BreakMinutes",
                    "[BreakMinutes] >= 0");

                table.HasCheckConstraint(
                    "CK_Shift_ToleranceMinutes",
                    "[ToleranceMinutes] >= 0");

                table.HasCheckConstraint(
                    "CK_Shift_Name_NotEmpty",
                    "LEN(LTRIM(RTRIM([Name]))) > 0");

                table.HasCheckConstraint(
                   "CK_Shift_Start_End",
                   "[StartTime] <> [EndTime]");
            });

            #endregion

            #region Properties

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.StartTime)
                .IsRequired();

            builder.Property(s => s.EndTime)
                .IsRequired();

            builder.Property(s => s.BreakMinutes)
                .HasDefaultValue(60);

            builder.Property(s => s.ToleranceMinutes)
                .HasDefaultValue(15);

            builder.Property(s => s.IsNightShift)
                .HasDefaultValue(false);

            builder.Property(s => s.IsActive)
                .HasDefaultValue(true);

            #endregion

            #region Indexes

            builder.HasIndex(s => s.Name)
                .IsUnique();

            #endregion

            #region Relationships

            builder.HasMany(s => s.Employees)
                .WithOne(e => e.Shift)
                .HasForeignKey(e => e.ShiftId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.Attendances)
                .WithOne(a => a.Shift)
                .HasForeignKey(a => a.ShiftId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion
        }
    }
}
