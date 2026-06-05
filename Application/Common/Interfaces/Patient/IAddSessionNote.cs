using Application.Commands.Patient;
using Application.Common.Responses;
using Application.DTOS.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Patient
{
    public interface IAddSessionNote
    {
        Task<BaseResponse<SessionNoteResponse>> AddNoteAsync(AddSessionNoteCommand command, CancellationToken ct);
    }
}
