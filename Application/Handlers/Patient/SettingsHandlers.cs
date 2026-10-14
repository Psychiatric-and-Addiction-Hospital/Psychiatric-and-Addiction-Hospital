using Application.Commands.Patient;
using Application.Common.Interfaces.Patient;
using Application.Common.Responses;
using Application.Queries.Patient;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Patient
{
    // PUT /api/Settings/security
    public class ChangePasswordHandler
        : IRequestHandler<ChangePasswordCommand, BaseResponse<Unit>>
    {
        private readonly ISettingsService _service;
        public ChangePasswordHandler(ISettingsService service) => _service = service;

        public Task<BaseResponse<Unit>> Handle(ChangePasswordCommand request, CancellationToken ct)
            => _service.ChangePasswordAsync(request, ct);
    }

    // GET /api/Settings/preferences
    public class GetPreferencesHandler
        : IRequestHandler<GetPreferencesQuery, BaseResponse<PreferencesResponse>>
    {
        private readonly ISettingsService _service;
        public GetPreferencesHandler(ISettingsService service) => _service = service;

        public Task<BaseResponse<PreferencesResponse>> Handle(GetPreferencesQuery request, CancellationToken ct)
            => _service.GetPreferencesAsync(request.UserId, ct);
    }

    // PUT /api/Settings/preferences
    public class UpdatePreferencesHandler
        : IRequestHandler<UpdatePreferencesCommand, BaseResponse<Unit>>
    {
        private readonly ISettingsService _service;
        public UpdatePreferencesHandler(ISettingsService service) => _service = service;

        public Task<BaseResponse<Unit>> Handle(UpdatePreferencesCommand request, CancellationToken ct)
            => _service.UpdatePreferencesAsync(request, ct);
    }
}
