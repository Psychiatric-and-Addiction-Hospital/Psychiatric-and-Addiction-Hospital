using Application.Commands.HR.Shift;
using Application.Common.Interfaces.HR.Shift;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Shift;
using Domain.Entites.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.shift
{
    public class CreateShiftService : ICreateShift
    {
        private readonly AddIdentityDbContext _context;

        public CreateShiftService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<ShiftResponse>> CreateAsync(CreateShiftCommand command, CancellationToken ct)
        {
            var exists = await _context.Shifts
                    .AnyAsync(x => x.Name == command.request.Name, ct);

            if (exists)
                return ResponseFactory.Fail<ShiftResponse>("Shift name already exists.");

            var shift = new Shift
            {
                Name = command.request.Name.Trim(),
                StartTime = command.request.StartTime,
                EndTime = command.request.EndTime,
                BreakMinutes = command.request.BreakMinutes,
                IsNightShift = command.request.IsNightShift,
                ToleranceMinutes = command.request.ToleranceMinutes,
                IsActive = true
            };

            await _context.Shifts.AddAsync(shift, ct);
            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(
                new ShiftResponse
                {
                    Id = shift.Id,
                    Name = shift.Name,
                    StartTime = shift.StartTime,
                    EndTime = shift.EndTime,
                    BreakMinutes = shift.BreakMinutes,
                    IsNightShift = shift.IsNightShift,
                    ToleranceMinutes = shift.ToleranceMinutes,
                    IsActive = shift.IsActive
                }, "Shift created successfully.");
        }
    }
}
