using Application.Commands.HR.ApplicationInterview;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationInterview;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.ApplicationInterview
{
    public interface ICreateApplicationInterview
    {
        Task<BaseResponse<ApplicationInterviewResponse>> CreateAsync(CreateApplicationInterviewCommand request, CancellationToken ct);
    }
}
