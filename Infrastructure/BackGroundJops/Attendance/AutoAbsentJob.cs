using Application.Common.Interfaces.BackgroundJobs;
using Infrastructure.services.HR.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BackGroundJops.Attendance
{
    public class AutoAbsentJob
    {
        private readonly IAutoAbsent _service;

        public AutoAbsentJob(
            IAutoAbsent service)
        {
            _service = service;
        }

        public async Task ExecuteAsync()
        {
            await _service.ExecuteAsync();
        }
    }
}
