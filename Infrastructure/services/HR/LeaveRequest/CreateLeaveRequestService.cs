using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.HR.LeaveRequest;
using Application.Common.Responses;
using Application.DTOS.Request.HR.LeaveRequest;
using Application.DTOS.Responses.HR.LeaveRequest;
using Infrastructure.Persistence.Identity;

namespace Infrastructure.services.HR.LeaveRequest
{
    public class CreateLeaveRequestService : ICreateLeaveRequest
    {
        private readonly AddIdentityDbContext _context;
        private readonly ILeaveRequestValidation _validation;
        public CreateLeaveRequestService(AddIdentityDbContext context, ILeaveRequestValidation validation)
        {
            _context = context;
            _validation = validation;
        }

        public Task<BaseResponse<LeaveRequestResponse>> CreateAsync(CreateLeaveRequest request, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
