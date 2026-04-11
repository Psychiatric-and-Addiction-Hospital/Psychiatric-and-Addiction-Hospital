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
    public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
       
            builder.ToTable("Candidates");

            

            builder.Property(c => c.FullName)
                .IsRequired()
                .HasMaxLength(200);

            
            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(256); 

           builder.HasIndex(c => c.Email)
                .IsUnique();

            builder.Property(c => c.Phone)
                .HasMaxLength(20) 
                .IsRequired(false); 

            builder.Property(c => c.ResumeUrl)
                .HasMaxLength(1000) 
                .IsRequired(false);
        }
    }
}
