using Application.Common.Responses;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Authentication
{
    public interface IPasswordResetService
    {
        Task<BaseResponse<string>> SendForgetPasswordOtpAsync(string email, CancellationToken ct);
    }
}
