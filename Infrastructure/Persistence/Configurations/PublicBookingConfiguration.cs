using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PublicBookingConfiguration: IEntityTypeConfiguration<PublicBooking>
    {
        public void Configure(EntityTypeBuilder<PublicBooking> builder)
        {
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(150);
            builder.Property(x => x.PhoneNumber).IsRequired();
            builder.Property(x => x.PreferredDate).IsRequired();
            builder.Property(x => x.PreferredTime).IsRequired();

            builder
                .HasOne(x => x.Doctor)
                .WithMany(d => d.PublicBookings)
                .HasForeignKey(x => x.DoctorId);
            builder.Property(A => A.Status).HasConversion<string>();
        }

    }
}
