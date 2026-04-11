using Domain.Entites.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations.HR
{
    public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
         
            builder.ToTable("Attendances");

            builder.Property(a => a.Date)
                .HasColumnType("date")
                .IsRequired();
           
            builder.Property(a => a.CheckIn)
                .IsRequired();

            builder.Property(a => a.CheckOut)
                .IsRequired(false);
          
            builder.HasIndex(a => new { a.EmployeeId, a.Date })
                .IsUnique();
           
            builder.HasOne(a => a.Employee)
                .WithMany(e => e.AttendanceLogs) 
                .HasForeignKey(a => a.EmployeeId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
