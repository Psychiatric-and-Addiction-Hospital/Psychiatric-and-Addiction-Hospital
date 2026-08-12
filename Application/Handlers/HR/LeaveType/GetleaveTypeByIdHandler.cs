using Application.Common.Interfaces.HR.LeaveType;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.LeaveType;
using Application.Queries.HR.LeaveType;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.LeaveType
{
    public class GetleaveTypeByIdHandler : IRequestHandler<GetleaveTypeByIdQuery, BaseResponse<LeaveTypeResponse>>
    {
        private readonly IGetleaveTypeById _service;
        public GetleaveTypeByIdHandler(IGetleaveTypeById service)
        {
            _service = service;
        }
        public async Task<BaseResponse<LeaveTypeResponse>> Handle(GetleaveTypeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
