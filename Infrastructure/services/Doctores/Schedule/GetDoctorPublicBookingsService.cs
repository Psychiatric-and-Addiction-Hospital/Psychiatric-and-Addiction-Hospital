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

        public GetDoctorPublicBookingsService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<List<PublicBookingResponse>>>GetBookings(Guid doctorId, CancellationToken ct)
        {
            var bookings = await _context.PublicBookings
                .Where(b => b.DoctorId == doctorId)
                .Select(b => new PublicBookingResponse
                {
                    Id = b.Id,
                    FullName = b.FullName,
                    PhoneNumber = b.PhoneNumber,
                    Email = b.Email,
                    PreferredDate = b.PreferredDate,
                    PreferredTime = b.PreferredTime,
                    Status = b.Status
                }).ToListAsync(ct);
            if(bookings == null || bookings.Count == 0)
            {
               return ResponseFactory.Fail<List<PublicBookingResponse>>("No bookings found for this doctor.");
            }
            return ResponseFactory.Success(bookings,"Successfully");

        }
    }
}