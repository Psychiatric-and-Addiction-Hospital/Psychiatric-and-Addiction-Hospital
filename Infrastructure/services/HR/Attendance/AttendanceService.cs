using Application.Common.Interfaces.HR.Attendance;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.Attendance
{
    public class AttendanceService : IAttendanceService
    {
        private readonly AddIdentityDbContext _context;

        public AttendanceService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<AttendanceResponse>> GetById(Guid id, CancellationToken ct)
        {
            var a = await _context.Attendances.FindAsync(new object[] { id }, ct);
            if (a == null)
                return ResponseFactory.Fail<AttendanceResponse>("Attendance not found");

            var res = new AttendanceResponse
            {
                Id = a.Id,
                EmployeeId = a.EmployeeId,
                Date = a.Date,
                CheckIn = a.CheckIn,
                CheckOut = a.CheckOut
            };

            return ResponseFactory.Success(res, "Attendance retrieved");
        }

        public async Task<BaseResponse<List<AttendanceResponse>>> GetAll(CancellationToken ct)
        {
            var records = await _context.Attendances
                .Select(a => new AttendanceResponse
                {
                    Id = a.Id,
                    EmployeeId = a.EmployeeId,
                    Date = a.Date,
                    CheckIn = a.CheckIn,
                    CheckOut = a.CheckOut
                }).ToListAsync(ct);

            return ResponseFactory.Success(records, records.Any() ? "Attendance records retrieved" : "No attendance records found");
        }

        public async Task<BaseResponse<AttendanceResponse>> UpdateAttendance(Guid id, DateTime? checkOut, CancellationToken ct)
        {
            var a = await _context.Attendances.FindAsync(new object[] { id }, ct);
            if (a == null)
                return ResponseFactory.Fail<AttendanceResponse>("Attendance not found");

            a.CheckOut = checkOut ?? DateTime.UtcNow;
            _context.Attendances.Update(a);
            await _context.SaveChangesAsync(ct);

            var res = new AttendanceResponse
            {
                Id = a.Id,
                EmployeeId = a.EmployeeId,
                Date = a.Date,
                CheckIn = a.CheckIn,
                CheckOut = a.CheckOut
            };

            return ResponseFactory.Success(res, "Attendance updated");
        }

        public async Task<BaseResponse<AttendanceResponse>> DeleteAttendance(Guid id, CancellationToken ct)
        {
            var a = await _context.Attendances.FindAsync(new object[] { id }, ct);
            if (a == null)
                return ResponseFactory.Fail<AttendanceResponse>("Attendance not found");

            _context.Attendances.Remove(a);
            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success<AttendanceResponse>(null, "Attendance deleted");
        }

        public async Task<BaseResponse<object>> CreateAttendance(Guid employeeId, CancellationToken ct)
        {
            var employeeExists = await _context.Employees.AnyAsync(e => e.Id == employeeId, ct);
            if (!employeeExists)
                return ResponseFactory.Fail<object>("Employee not found");

            var today = DateTime.UtcNow.Date;
            var exists = await _context.Attendances.AnyAsync(a => a.EmployeeId == employeeId && a.Date == today, ct);
            if (exists)
                return ResponseFactory.Fail<object>("Attendance for today already recorded");

            var attendance = new Domain.Entites.HR.Attendance
            {
                EmployeeId = employeeId,
                Date = today,
                CheckIn = DateTime.UtcNow
            };

            await _context.Attendances.AddAsync(attendance, ct);
            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success<object>(null, "Check-in recorded");
        }

        public async Task<BaseResponse<List<AttendanceResponse>>> GetByEmployee(Guid employeeId, CancellationToken ct)
        {
            var records = await _context.Attendances
                .Where(a => a.EmployeeId == employeeId)
                .Select(a => new AttendanceResponse
                {
                    Id = a.Id,
                    EmployeeId = a.EmployeeId,
                    Date = a.Date,
                    CheckIn = a.CheckIn,
                    CheckOut = a.CheckOut
                }).ToListAsync(ct);

            return ResponseFactory.Success(records, records.Any() ? "Attendance records retrieved" : "No attendance records found");
        }
    }
}
