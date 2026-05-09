namespace Infrastructure.Persistence.Configurations.HR
{
    using Domain.Entites.HR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    namespace Infrastructure.Data.Configurations
    {
        public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
        {
            public void Configure(EntityTypeBuilder<Employee> builder)
            {
                builder.ToTable("Employees");

                
                builder.Property(e => e.EmployeeCode)
                    .IsRequired()
                    .HasMaxLength(50);

               
                builder.HasIndex(e => e.EmployeeCode)
                    .IsUnique();

                builder.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                builder.HasOne(e => e.Department)
                    .WithMany(d => d.Employees)
                    .HasForeignKey(e => e.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);


                builder.HasOne(e => e.Manager)
                    .WithMany()
                    .HasForeignKey(e => e.ManagerId)
                    .OnDelete(DeleteBehavior.Restrict);


                builder.HasOne(e => e.user)
                    .WithOne()
                    .HasForeignKey<Employee>(e => e.UserId)
                    .OnDelete(DeleteBehavior.SetNull);


                builder.HasOne(e => e.Contract)
                    .WithOne(c => c.Employee)
                    .HasForeignKey<Contract>("EmployeeId")
                    .OnDelete(DeleteBehavior.Cascade);


                builder.HasMany(e => e.AttendanceLogs)
                    .WithOne(a => a.Employee)
                    .HasForeignKey("EmployeeId")
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasMany(e => e.Payrolls)
                    .WithOne(p => p.Employee)
                    .HasForeignKey("EmployeeId")
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasMany(e => e.ManagedRecruitments)
                    .WithOne()
                    .HasForeignKey("ManagerId")
                    .OnDelete(DeleteBehavior.Restrict);
            }
        }
    }
}
