using Domain.Entites.HR.Applications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations.HR
{ 
    public class ApplicationProcessConfiguration : IEntityTypeConfiguration<ApplicationProcess>
        {
            public void Configure(EntityTypeBuilder<ApplicationProcess> builder)
            {
             
                builder.ToTable("ApplicationProcesses");

              
                builder.Property(a => a.States)
                    .HasConversion<string>()
                    .HasMaxLength(50)
                    .IsRequired();

               builder.HasOne(a => a.Candidate)
                    .WithMany()
                    .HasForeignKey(a => a.CandidateId)
                    
                    .OnDelete(DeleteBehavior.Cascade);
                    builder.HasOne(a => a.Recruitment)
                    .WithMany() 
                    .HasForeignKey(a => a.RecruitmentId)
                    .OnDelete(DeleteBehavior.Restrict);
            }
    
    }
    
}
