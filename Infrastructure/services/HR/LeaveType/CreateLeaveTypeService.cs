using Application.Common.Interfaces.HR.LeaveType;
using Application.Common.Responses;
using Application.DTOS.Request.HR.LeaveType;
using Application.DTOS.Responses.HR.LeaveType;
using Infrastructure.Persistence.Identity;


namespace Infrastructure.services.HR.LeaveType
{
    public class CreateLeaveTypeService : ICreateLeaveType
    {
        private readonly AddIdentityDbContext _context;

        private readonly ILeaveTypeValidation _validation;

        public CreateLeaveTypeService(
            AddIdentityDbContext context,
            ILeaveTypeValidation validation)
        {
            _context = context;
            _validation = validation;
        }

        public async Task<BaseResponse<LeaveTypeResponse>> CreateAsync(CreateLeaveTypeRequest request, CancellationToken ct)
        {
            var validation = await _validation.ValidateCreateAsync(request, ct);

            if (!validation.Success)
                return ResponseFactory.Fail<LeaveTypeResponse>(validation.Message, validation.Errors);

            var leaveType = new Domain.Entites.HR.Leave.LeaveType
            {
                Name = request.Name.Trim(),

                Description = request.Description?.Trim(),

                MaxDaysPerYear = request.MaxDaysPerYear,

                IsPaid = request.IsPaid,

                RequiresApproval = request.RequiresApproval,

                AllowHalfDay = request.AllowHalfDay
            };

            _context.LeaveTypes.Add(leaveType);

            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(new LeaveTypeResponse
            {
                Id = leaveType.Id,

                Name = leaveType.Name,

                Description = leaveType.Description,

                MaxDaysPerYear = leaveType.MaxDaysPerYear,

                IsPaid = leaveType.IsPaid,

                RequiresApproval = leaveType.RequiresApproval,

                AllowHalfDay = leaveType.AllowHalfDay,

                IsActive = leaveType.IsActive
            },
            "Leave type created successfully.");
        }
    }
}