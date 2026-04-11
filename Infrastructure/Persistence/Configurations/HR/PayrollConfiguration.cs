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
    public class PayrollConfiguration : IEntityTypeConfiguration<Payroll>
    {
        public void Configure(EntityTypeBuilder<Payroll> builder)
        {
        
            builder.ToTable("Payrolls", t =>
            {
               
                t.HasCheckConstraint("CK_Payroll_GrossPay", "[GrossPay] >= 0");
                t.HasCheckConstraint("CK_Payroll_Deductions", "[Deductions] >= 0");
                t.HasCheckConstraint("CK_Payroll_OvertimeRate", "[OvertimeSefite] >= 0");
            });

           
            builder.Property(p => p.PaymentDate)
                .HasColumnType("date")
                .IsRequired();

            
            builder.Property(p => p.OverSefit) 
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.OvertimeSefite)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.GrossPay)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.Deductions)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Ignore(p => p.NetPay);

            builder.HasOne(p => p.Employee)
                .WithMany(e => e.Payrolls) 
                .HasForeignKey(p => p.EmployeeId)
              
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
