using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Doctores.Schedule;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.Doctores.Schedule
{
    public class GetDoctorPublicBookingsService : IGetDoctorPublicBookings
    {
        private readonly AddIdentityDbContext _context;
        private readonly ICurrentUser _currentUser;

        public GetDoctorPublicBookingsService(AddIdentityDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<List<PublicBookingResponse>>> GetBookings(CancellationToken ct)
        {
            if (!_currentUser.IsAuthenticated)
                return ResponseFactory.Fail<List<PublicBookingResponse>>("User must be authenticated.");

            var userId = _currentUser.UserId;

            if (string.IsNullOrWhiteSpace(userId))
                return ResponseFactory.Fail<List<PublicBookingResponse>>("Authenticated user must have a valid user ID.");

            var bookings = await _context.PublicBookings
                .Where(b => b.Doctor.Employee.AppUserId == userId)
                .Select(b => new PublicBookingResponse
                {
                    Id = b.Id,
                    FullName = b.FullName,
                    PhoneNumber = b.PhoneNumber,
                    Email = b.Email,
                    PreferredDate = b.PreferredDate,
                    PreferredTime = b.PreferredTime,
                    DoctorName = $"{b.Doctor.Employee.FirstName}{b.Doctor.Employee.LastName}",
                    DoctorId = b.DoctorId,

                    Status = b.Status
                }).ToListAsync(ct);
            if (bookings == null || bookings.Count == 0)
            {
                return ResponseFactory.Fail<List<PublicBookingResponse>>("No bookings found for this doctor.");
            }
            return ResponseFactory.Success(bookings, "Successfully");

        }
    }
}