using Application.Common.Interfaces.HR.Position;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.Position
{
    public class GetPositionByIdService : IGetPositionById
    {
        private readonly AddIdentityDbContext _context;

        public GetPositionByIdService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<PositionResponse>> GetByIdAsync(
            Guid id,
            CancellationToken ct)
        {
            var position = await _context.Positions
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new PositionResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    BasicSalary = p.BasicSalary,
                    IsActive = p.IsActive,
                    DepartmentId = p.DepartmentId
                })
                .FirstOrDefaultAsync(ct);

            if (position is null)
                return ResponseFactory.Fail<PositionResponse>("Position not found.");

            return ResponseFactory.Success(position, "Position retrieved successfully.");
        }
    }
}


