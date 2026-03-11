using Domain.Entites.DoctorsModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    internal class DoctorScheduleConfiguration : IEntityTypeConfiguration<DoctorSchedule>
    {

        public void Configure(EntityTypeBuilder<DoctorSchedule> builder)
        {
            builder.HasOne(ds => ds.DoctorProfile)
                .WithMany(dp => dp.Schedules)
                .HasForeignKey(ds => ds.DoctorProfileId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}



