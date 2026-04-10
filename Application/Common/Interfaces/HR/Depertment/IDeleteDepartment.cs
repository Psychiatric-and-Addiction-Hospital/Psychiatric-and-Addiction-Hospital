using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Depertment
{
    public interface IDeleteDepartment
    {
        Task<BaseResponse<DepertmentResponse>> DeleteDepartment(Guid Id,CancellationToken ct);
    }
}
