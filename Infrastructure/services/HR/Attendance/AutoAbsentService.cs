using Application.Common.Interfaces.BackgroundJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.Attendance
{
    public class AutoAbsentService : IAutoAbsent
    {
        public Task ExecuteAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
