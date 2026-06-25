using Domain.Entites.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    internal class ChatMassageConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.ToTable("ChatMessages");
            
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Message)
                   .HasMaxLength(2000)
                   .IsRequired(false);

            builder.Property(x => x.SentAt)
                   .IsRequired();

            builder.Property(x => x.Status)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(x => x.Type)
                  .HasConversion<int>()
                  .IsRequired();

            builder.Property(x => x.MediaUrl)
                   .HasMaxLength(400)
                   .IsRequired(false);

            builder.Property(x => x.IsRead)
                   .HasDefaultValue(false);

            builder.HasOne(C => C.Sender)
                .WithMany(A => A.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(C => C.Receiver)
            .WithMany(A => A.ReceivedMessages)
            .HasForeignKey(m => m.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(x => x.ConversationId);

            builder.HasIndex(x => new { x.SenderId, x.ReceiverId });
        }
    }
}
