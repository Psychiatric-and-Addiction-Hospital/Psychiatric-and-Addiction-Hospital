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
    public  class RecruitmentConfiguration :IEntityTypeConfiguration<Recruitment>
    {
        public void Configure(EntityTypeBuilder<Recruitment> builder)

        {
            builder.ToTable("Recruitments");

            builder.Property(x=> x.Title)
                .IsRequired()
                .HasMaxLength(50); 

            builder.Property(x=> x.Description)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(r => r.ExperienceLevel)
                  .IsRequired()
                  .HasMaxLength(50);

            builder.Property(r => r.MinSalary)
                .HasColumnType("decimal(18,2)");

            builder.Property(r => r.MaxSalary)
                .HasColumnType("decimal(18,2)");
            
            builder.HasOne(r => r.Department)
                .WithMany() 
                .HasForeignKey(r => r.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.HiringManager)
                .WithMany(e => e.ManagedRecruitments) 
                .HasForeignKey(r => r.HiringManagerId)
                .OnDelete(DeleteBehavior.Restrict);




        }

    }
}
