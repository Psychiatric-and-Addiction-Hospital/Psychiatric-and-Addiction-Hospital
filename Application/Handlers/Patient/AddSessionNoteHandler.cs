using Application.Commands.Patient;
using Application.Common.Interfaces.Patient;
using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Patient
{
    public class AddSessionNoteHandler : IRequestHandler<AddSessionNoteCommand, BaseResponse<SessionNoteResponse>>
    {
        private readonly IAddSessionNote _service;

        public AddSessionNoteHandler(IAddSessionNote service)
        {
            _service = service;
        }

        public async Task<BaseResponse<SessionNoteResponse>> Handle(AddSessionNoteCommand request, CancellationToken ct)
        {
            return await _service.AddNoteAsync(request, ct);
        }
    }
}
