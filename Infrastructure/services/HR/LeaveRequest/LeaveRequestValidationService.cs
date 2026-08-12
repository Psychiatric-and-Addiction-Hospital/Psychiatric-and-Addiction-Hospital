using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.HR.LeaveRequest;
using Application.Common.Responses;
using Application.DTOS.Request.HR.LeaveRequest;
using Infrastructure.Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.LeaveRequest
{
    public class LeaveRequestValidationService : ILeaveRequestValidation
    {
        private readonly AddIdentityDbContext _context;
        private readonly ICurrentUser _currentUser;
        public LeaveRequestValidationService(AddIdentityDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }   
        public Task<BaseResponse<Domain.Entites.HR.Employee>> ValidateCreateAsync(CreateLeaveRequest request, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
