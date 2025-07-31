using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasOne(N => N.RelatedSession)
                 .WithMany(S => S.Notifications)
                 .HasForeignKey(N => N.RelatedSessionId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(N => N.Recipient)
             .WithMany(S => S.Notifications)
             .HasForeignKey(N => N.RecipientId)
             .OnDelete(DeleteBehavior.SetNull);

            builder.Property(n => n.NotificationType)
                 .HasConversion<int>();
        }
    }
}
