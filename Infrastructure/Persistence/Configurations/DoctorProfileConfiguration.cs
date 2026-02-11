using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class DoctorProfileConfiguration : IEntityTypeConfiguration<DoctorProfile>
    {
        public void Configure(EntityTypeBuilder<DoctorProfile> builder)
        {
            builder.HasOne(D => D.User)
                 .WithOne(A => A.DoctorProfile)
                 .HasForeignKey<DoctorProfile>(P => P.UserId)
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
