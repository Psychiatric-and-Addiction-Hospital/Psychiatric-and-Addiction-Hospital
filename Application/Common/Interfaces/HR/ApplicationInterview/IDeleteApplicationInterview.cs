using Application.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.ApplicationInterview
{
    public interface IDeleteApplicationInterview
    {
        Task<BaseResponse<bool>> DeleteAsync(Guid InterviewId, CancellationToken ct);
    }
}
