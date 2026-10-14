using Application.Common.Interfaces.EmployeeProfile;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Employee;
using Application.Queries.EmployeeProfile;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.EmployeeProfile
{
    public class GetMyProfileHandler : IRequestHandler<GetMyProfileQuery, BaseResponse<EmployeeResponse>>
    {
        private readonly IGetMyProfile _service;
        public GetMyProfileHandler(IGetMyProfile service)
        {
            _service = service;
        }

        public async Task<BaseResponse<EmployeeResponse>> Handle(GetMyProfileQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAsync(cancellationToken);
        }
    }
}
