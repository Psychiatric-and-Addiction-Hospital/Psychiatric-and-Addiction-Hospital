using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    .HasMaxLength(256);

             
                builder.HasIndex(e => e.Email)
                    .IsUnique();

                builder.Property(e => e.IsActive)
                    .HasDefaultValue(true);

                

                
                builder.HasOne(e => e.user)
                    .WithMany() 
                    .HasForeignKey(e => e.UserId)
                    .IsRequired(false) 
                    .OnDelete(DeleteBehavior.SetNull);

      
                builder.HasOne(e => e.Department)
                    .WithMany() 
                    .HasForeignKey(e => e.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict); 

             
                builder.HasOne(e => e.Contract)
                    .WithOne() 
                    .HasForeignKey<Contract>("EmployeeId") 
                    .OnDelete(DeleteBehavior.Cascade);

                
                builder.HasMany(e => e.AttendanceLogs)
                    .WithOne()
                    .HasForeignKey("EmployeeId") 
                    .OnDelete(DeleteBehavior.Cascade);

               
                builder.HasMany(e => e.Payrolls)
                    .WithOne()
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
