using Application.Commands.HR.Application;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Application;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Application
{
    public interface ICreateApplication
    {
        Task<BaseResponse<ApplicationResponse>> CreateAsync(
       CreateApplicationCommand request,
       CancellationToken ct);
    }
}
