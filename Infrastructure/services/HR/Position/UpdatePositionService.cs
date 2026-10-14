using Application.Common.Interfaces.HR.Position;
using Application.Common.Responses;
using Application.DTOS.Request.HR.Position;
using Application.DTOS.Responses.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.Position
{
    public class UpdatePositionService : IUpdatePosition
    {
        private readonly AddIdentityDbContext _context;

        public UpdatePositionService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<PositionResponse>> UpdatePositionAsync(UpdatePositionRequest request, CancellationToken ct)
        {
            request.Name = request.Name.Trim();

            var position = await _context.Positions
                  .Include(p => p.Department)
                .FirstOrDefaultAsync(p => p.Id == request.Id, ct);

            if (position is null)
            {
                return ResponseFactory.Fail<PositionResponse>(
                    "Position not found.");
            }

            bool departmentExists = await _context.Departments
                .AnyAsync(d => d.Id == request.DepartmentId, ct);

            if (!departmentExists)
            {
                return ResponseFactory.Fail<PositionResponse>(
                    "Department not found.");
            }

            bool exists = await _context.Positions.AnyAsync(
                p => p.Id != request.Id &&
                     p.DepartmentId == request.DepartmentId &&
                     p.Name == request.Name,
                ct);

            if (exists)
            {
                return ResponseFactory.Fail<PositionResponse>(
                    "Position already exists in this department.");
            }

            position.Name = request.Name;
            position.Description = request.Description?.Trim();
            position.BasicSalary = request.BasicSalary;
            position.DepartmentId = request.DepartmentId;

            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(new PositionResponse
            {
                Id = position.Id,
                Name = position.Name,
                Description = position.Description,
                BasicSalary = position.BasicSalary,
                IsActive = position.IsActive,
                DepartmentId = position.DepartmentId,
                DepartmentName = position.Department.Name
            },
                "Position updated successfully.");
        }
    }
}