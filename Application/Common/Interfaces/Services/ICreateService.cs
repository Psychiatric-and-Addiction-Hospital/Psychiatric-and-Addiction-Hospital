using Application.Common.Responses;
using Application.DTOS.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface ICreateService
    {
        Task<BaseResponse<ServiceResponse>> CreateAsync(string name, string description, Guid deptId, CancellationToken ct);
    }
}
