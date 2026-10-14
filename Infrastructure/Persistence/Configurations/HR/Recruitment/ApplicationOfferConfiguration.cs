using Domain.Entites.HR;
using Domain.Entites.HR.Recruitment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.HR.Recruitment
{
    public class ApplicationOfferConfiguration : IEntityTypeConfiguration<ApplicationOffer>
    {
        public void Configure(EntityTypeBuilder<ApplicationOffer> builder)
        {
            builder.ToTable("ApplicationOffers", table =>
            {
                table.HasCheckConstraint(
                    "CK_ApplicationOffer_OfferedSalary",
                    "[OfferedSalary] >= 0");

                table.HasCheckConstraint(
                    "CK_ApplicationOffer_Dates",
                    "[ExpiryDate] >= [OfferDate]");

                table.HasCheckConstraint(
                    "CK_ApplicationOffer_ResponseDate",
                    "[ResponseDate] IS NULL OR [ResponseDate] >= [OfferDate]");
            });

            #region Properties

            builder.Property(o => o.OfferedSalary)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(o => o.OfferDate)
                .IsRequired();

            builder.Property(o => o.ExpiryDate)
                .IsRequired();

            builder.Property(o => o.ResponseDate);

            builder.Property(o => o.Status)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(o => o.Notes)
                .HasMaxLength(2000);

            builder.Property(x => x.RejectionReason)
                .HasMaxLength(1000);

            builder.Property(x => x.OfferDocumentUrl)
                .HasMaxLength(500);

            #endregion

            #region Indexes

            builder.HasIndex(o => o.ApplicationId)
                .IsUnique();

            builder.HasIndex(o => o.Status);

            builder.HasIndex(o => o.ApprovedByEmployeeId);

            #endregion

            #region Relationships

            // Application 1 ---- 1 Offer
            builder.HasOne(o => o.Application)
                .WithOne(a => a.Offer)
                .HasForeignKey<ApplicationOffer>(o => o.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            // HR Employee who approved the offer
            builder.HasOne(o => o.ApprovedByEmployee)
                .WithMany()
                .HasForeignKey(o => o.ApprovedByEmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Offer 1 ---- 1 Contract
            builder.HasOne(o => o.Contract)
                .WithOne(c => c.Offer)
                .HasForeignKey<Contract>(c => c.OfferId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion
        }
    }

}

