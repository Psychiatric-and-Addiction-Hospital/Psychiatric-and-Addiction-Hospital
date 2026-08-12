using Domain.Entites.HR.Leave;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.HR.Leave
{
    public class EmployeeLeaveBalanceConfiguration : IEntityTypeConfiguration<EmployeeLeaveBalance>
    {
        public void Configure(EntityTypeBuilder<EmployeeLeaveBalance> builder)
        {

            builder.HasIndex(x => new
            {
                x.EmployeeId,
                x.LeaveTypeId
            }).IsUnique();

            builder.HasOne(x => x.LeaveType)
                .WithMany(x => x.EmployeeLeaveBalances)
                .HasForeignKey(x => x.LeaveTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Employee)
              .WithMany(x => x.LeaveBalances)
              .HasForeignKey(x => x.EmployeeId)
              .OnDelete(DeleteBehavior.Cascade);



        }
    }
}
