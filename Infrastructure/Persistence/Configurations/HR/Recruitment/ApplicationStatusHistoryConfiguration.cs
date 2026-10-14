using Domain.Entites.HR.Recruitment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.HR.Recruitment
{
    public class ApplicationStatusHistoryConfiguration : IEntityTypeConfiguration<ApplicationStatusHistory>
    {
        public void Configure(EntityTypeBuilder<ApplicationStatusHistory> builder)
        {
            builder.HasOne(x => x.Application)
                .WithMany(x => x.StatusHistory)
                .HasForeignKey(x => x.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
