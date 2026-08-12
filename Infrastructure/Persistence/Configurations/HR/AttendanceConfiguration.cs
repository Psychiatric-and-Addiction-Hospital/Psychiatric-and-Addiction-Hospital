using Domain.Entites.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.HR
{
    public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.ToTable("Attendances", table =>
            {
                table.HasCheckConstraint(
                    "CK_Attendance_LateMinutes",
                    "[LateMinutes] >= 0");
            });

            #region Properties

            builder.Property(a => a.AttendanceDate)
                .IsRequired();

            builder.Property(a => a.CheckInTime)
                .IsRequired(false);

            builder.Property(a => a.CheckOutTime)
                .IsRequired(false);

            builder.Property(a => a.ActualWorkedTime)
                .HasColumnType("time")
                .HasDefaultValue(TimeSpan.Zero);


            builder.Property(a => a.LateMinutes)
                .HasDefaultValue(0);

            builder.Property(a => a.Overtime)
    .HasColumnType("time")
    .HasDefaultValue(TimeSpan.Zero);

            builder.Property(a => a.EarlyLeaveMinutes)
                .HasDefaultValue(0);

            builder.Property(a => a.IsLocked)
                .HasDefaultValue(false);



            builder.Property(a => a.AttendanceStatus)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            #endregion

            #region Indexes

            builder.HasIndex(a => new
            {
                a.EmployeeId,
                a.AttendanceDate
            }).IsUnique();

            builder.HasIndex(a => a.AttendanceStatus);

            builder.HasIndex(a => a.ShiftId);

            #endregion

            #region Relationships

            builder.HasOne(a => a.Employee)
                .WithMany(e => e.Attendances)
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Shift)
                .WithMany(s => s.Attendances)
                .HasForeignKey(a => a.ShiftId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion
        }
    }
}

