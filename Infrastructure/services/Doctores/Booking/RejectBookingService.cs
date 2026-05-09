using Application.Common.Interfaces.Doctores.Booking;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Application.DTOS.Responses.booking;   
using Domain.Entites.Features;
using Domain.Enums;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using PublicBookingResponse = Application.DTOS.Responses.PublicBookingResponse;

namespace Infrastructure.services.Doctores.Booking
{
    public class RejectBookingService : IRejectBooking
    {
        private readonly AddIdentityDbContext _context;

        public RejectBookingService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<Application.DTOS.Responses.PublicBookingResponse>> RejectBookingAsync(
            Guid bookingId, string? rejectionReason, CancellationToken ct)
        {
           
            var booking = await _context.PublicBookings
                .Include(b => b.Doctor)
                    .ThenInclude(d => d.User)
                .Include(b => b.Schedule)
                .FirstOrDefaultAsync(b => b.Id == bookingId, ct);

            if (booking == null)
            {
                return ResponseFactory.Fail<Application.DTOS.Responses.PublicBookingResponse>("Booking not found",
                    new System.Collections.Generic.List<string> { "The provided bookingId does not match any existing booking." });
            }

            if (booking.Status != Status.Pending)
            {
                return ResponseFactory.Fail<PublicBookingResponse>(
                    $"Booking has already been {booking.Status}",
                    new System.Collections.Generic.List<string> { "Only pending bookings can be rejected." });
            }

            // 2. Change booking status → Rejected
            booking.Status = Status.Rejected;
            booking.RejectionReason = rejectionReason;

            // 3. Reopen the schedule slot
            if (booking.Schedule != null)
            {
                booking.Schedule.IsBooked = false;
            }

            // 4. Send Notification with rejection
            var patientUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == booking.Email, ct);

            if (patientUser != null)
            {
                var message = $"Your booking with Dr. {booking.Doctor.User.FirstName} {booking.Doctor.User.LastName} " +
                              $"on {booking.PreferredDate:yyyy-MM-dd} at {booking.PreferredTime} has been rejected.";

                if (!string.IsNullOrWhiteSpace(rejectionReason))
                {
                    message += $" Reason: {rejectionReason}";
                }

                var notification = new Notification
                {
                    Title = "Booking Rejected ",
                    Message = message,
                    SentAt = DateTime.UtcNow,
                    IsRead = false,
                    NotificationType = NotificationType.BookingRejected,
                    RecipientId = patientUser.Id
                };

                await _context.Notifications.AddAsync(notification, ct);
            }

            await _context.SaveChangesAsync(ct);

            // 5. Return response
            return ResponseFactory.Success(new PublicBookingResponse
            {
                Id = booking.Id,
                FullName = booking.FullName,
                PhoneNumber = booking.PhoneNumber,
                Email = booking.Email,
                DoctorId = booking.DoctorId,
                DoctorName = $"{booking.Doctor.User.FirstName} {booking.Doctor.User.LastName}",
                PreferredDate = booking.PreferredDate,
                PreferredTime = booking.PreferredTime,
                Status = booking.Status,
                RejectionReason = booking.RejectionReason
            }, "Booking rejected successfully");
        }

       

        Task<BaseResponse<RejectBookingResponse>> IRejectBooking.RejectBookingAsync(Guid bookingId, string rejectionReason, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
