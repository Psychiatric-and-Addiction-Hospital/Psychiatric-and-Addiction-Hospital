using Application.Common.Interfaces.HR.Position;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.Position
{
    public class DeletePositionService : IDeletePosition
    {
        private readonly AddIdentityDbContext _context;

        public DeletePositionService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<PositionResponse>> DeletePositionAsync(
            Guid id,
            CancellationToken ct)
        {
            var position = await _context.Positions
                .Include(p => p.Employees)
                .Include(p => p.JobPostings)
                .FirstOrDefaultAsync(p => p.Id == id, ct);

            if (position is null)
            {
                return ResponseFactory.Fail<PositionResponse>(
                    "Position not found.");
            }

            if (position.Employees.Any())
            {
                return ResponseFactory.Fail<PositionResponse>(
                    "Cannot delete position because it has employees assigned.");
            }

            if (position.JobPostings.Any())
            {
                return ResponseFactory.Fail<PositionResponse>(
                    "Cannot delete position because it has job postings.");
            }

            _context.Positions.Remove(position);

            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(
                new PositionResponse
                {
                    Id = position.Id,
                    Name = position.Name,
                    Description = position.Description
                },
                "Position deleted successfully.");
        }
    }
}
