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
    public  class ApplicationInterviewConfiguration: IEntityTypeConfiguration<ApplicationInterview>
    {
        public void Configure (EntityTypeBuilder<ApplicationInterview>builder)
        {
            builder.ToTable("ApplicationInterview");

            builder.Property(V => V.interviewType)
               .HasConversion<string>()
               .IsRequired();
               
          
            builder.Property(x => x.InterviewerName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(i => i.Feedback)
                .HasMaxLength(2000)
                .IsRequired(false);
            builder.Property(i => i.Location)
                .HasMaxLength(200)
                .IsRequired(false);
            builder.Property(h=>h.Score).IsRequired()
                .HasMaxLength(100);
           
            builder.HasOne(i => i.ApplicationProcess)
                .WithMany() 
                .HasForeignKey(i => i.ApplicationProcessId)
               .OnDelete(DeleteBehavior.Cascade);

        }


    }
}
