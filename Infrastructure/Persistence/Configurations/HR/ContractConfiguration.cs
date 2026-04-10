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
    public class ContractualConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {

            builder.ToTable("Contracts", t =>
            {

                t.HasCheckConstraint("CK_Contract_Dates", "[EndDate] IS NULL OR [EndDate] >= [StartDate]");


                t.HasCheckConstraint("CK_Contract_BaseSalary", "[BaseSalary] >= 0");
            });




            builder.Property(c => c.StartDate)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(c => c.EndDate)
                .HasColumnType("date")
                .IsRequired(false); 

            builder.Property(c => c.Terms)
                .IsRequired(false) 
                .HasMaxLength(4000); 

          
            builder.Property(c => c.BaseSalary)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

        
            builder.HasOne(c => c.Employee)
                .WithOne(e => e.Contract) 
                .HasForeignKey<Contract>(c => c.EmployeeId)
                
                .OnDelete(DeleteBehavior.Cascade);

         
        }
   
    }
}
