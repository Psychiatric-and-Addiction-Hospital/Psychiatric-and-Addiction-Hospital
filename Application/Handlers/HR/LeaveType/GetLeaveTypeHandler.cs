using Application.Common.Responses;
using Application.DTOS.Responses.HR.LeaveType;
using Application.Queries.HR.LeaveType;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.LeaveType
{
    public class GetLeaveTypeHandler : IRequestHandler<GetLeaveTypesQuery, BaseResponse<PagedResponse<LeaveTypeResponse>>>
    {
        public Task<BaseResponse<PagedResponse<LeaveTypeResponse>>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
