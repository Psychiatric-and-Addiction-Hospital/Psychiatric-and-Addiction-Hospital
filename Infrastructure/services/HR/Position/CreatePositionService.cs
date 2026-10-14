using Application.Common.Interfaces.HR.Position;
using Application.Common.Responses;
using Application.DTOS.Request.HR.Position;
using Application.DTOS.Responses.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.Position
{
    public class CreatePositionService : ICreatePosition
    {
        private readonly AddIdentityDbContext _Context;
        public CreatePositionService(AddIdentityDbContext context)
        {
            _Context = context;
        }
        public async Task<BaseResponse<PositionResponse>> CreatePositionAsync(CreatePositionRequest request, CancellationToken ct)
        {
            request.Name = request.Name.Trim();

            bool departmentExists = await _Context.Departments
                .AnyAsync(d => d.Id == request.DepartmentId, ct);

            if (!departmentExists)
            {
                return ResponseFactory.Fail<PositionResponse>(
                    "Department not found.");
            }

            bool exists = await _Context.Positions.AnyAsync(
                p => p.DepartmentId == request.DepartmentId &&
                     p.Name.ToLower() == request.Name.ToLower(),
                ct);

            if (exists)
            {
                return ResponseFactory.Fail<PositionResponse>(
                    "Position already exists in this department.");
            }

            var position = new Domain.Entites.HR.Position
            {
                Name = request.Name,
                Description = request.Description?.Trim(),
                BasicSalary = request.BasicSalary,
                DepartmentId = request.DepartmentId,
            };

            await _Context.Positions.AddAsync(position, ct);

            await _Context.SaveChangesAsync(ct);

            return ResponseFactory.Success(new PositionResponse
            {
                Id = position.Id,
                Name = position.Name,
                Description = position.Description,
                BasicSalary = position.BasicSalary,
                IsActive = position.IsActive,
                DepartmentId = position.DepartmentId,
                DepartmentName = position.Department?.Name
            }, "Position created successfully.");

        }


    }
}
