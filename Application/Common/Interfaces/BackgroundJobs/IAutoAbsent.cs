using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.BackgroundJobs
{
    public interface IAutoAbsent
    {
        Task ExecuteAsync(CancellationToken ct = default);
    }
}
