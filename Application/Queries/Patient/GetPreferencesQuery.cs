using Application.Common.Responses;
using MediatR;

namespace Application.Queries.Patient
{
    // GET /api/Settings/preferences
    public record GetPreferencesQuery(string UserId)
        : IRequest<BaseResponse<PreferencesResponse>>;
}
