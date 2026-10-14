using Domain.Entites.DoctorsModule;
using Domain.Entites.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Persistence.Configurations.HR
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            #region Table

            builder.ToTable("Employees");

            #endregion

            #region Properties

            builder.Property(e => e.EmployeeCode)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(e => e.NationalId)
               .IsRequired()
               .HasMaxLength(14);

            builder.Property(e => e.PhoneNumber)
                .HasMaxLength(20);


            builder.Property(e => e.IsActive)
                .HasDefaultValue(true);

            builder.Property(e => e.HireDate)
                .IsRequired();

            builder.Property(e => e.EmploymentStatus)
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(e => e.EmergencyContactName)
               .HasMaxLength(200);

            builder.Property(e => e.EmergencyContactPhone)
                .HasMaxLength(20);

            #endregion

            #region Indexes

            builder.HasIndex(e => e.EmployeeCode)
                .IsUnique();

            builder.HasIndex(e => e.Email)
                .IsUnique();

            builder.HasIndex(e => e.AppUserId)
                .IsUnique();

            builder.HasIndex(e => e.NationalId)
                .IsUnique();

            #endregion

            #region Relationships

            // Department
            builder.HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Position
            builder.HasOne(e => e.Position)
                .WithMany(p => p.Employees)
                .HasForeignKey(e => e.PositionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Shift
            builder.HasOne(e => e.Shift)
                .WithMany(s => s.Employees)
                .HasForeignKey(e => e.ShiftId)
                .OnDelete(DeleteBehavior.Restrict);

            // Identity User
            builder.HasOne(e => e.AppUser)
                .WithOne()
                .HasForeignKey<Employee>(e => e.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Manager (Self Reference)
            builder.HasOne(e => e.Manager)
                .WithMany(e => e.Subordinates)
                .HasForeignKey(e => e.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Doctor Profile
            builder.HasOne(e => e.DoctorProfile)
                .WithOne(d => d.Employee)
                .HasForeignKey<DoctorProfile>(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);


            // Attendance
            builder.HasMany(e => e.Attendances)
                .WithOne(a => a.Employee)
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Payroll
            builder.HasMany(e => e.Payrolls)
                .WithOne(p => p.Employee)
                .HasForeignKey(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Leave Requests
            builder.HasMany(e => e.LeaveRequests)
                .WithOne(l => l.Employee)
                .HasForeignKey(l => l.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Performance Reviews
            builder.HasMany(e => e.PerformanceReviews)
                .WithOne(r => r.Employee)
                .HasForeignKey(r => r.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Constraints

            builder.HasCheckConstraint(
                "CK_Employee_FirstName_NotEmpty",
                "LEN(LTRIM(RTRIM([FirstName]))) > 0");

            builder.HasCheckConstraint(
                "CK_Employee_LastName_NotEmpty",
                "LEN(LTRIM(RTRIM([LastName]))) > 0");

            #endregion
        }
    }
}

