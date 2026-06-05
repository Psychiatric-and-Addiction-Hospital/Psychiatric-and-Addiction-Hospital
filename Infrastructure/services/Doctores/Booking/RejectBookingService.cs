using Application.Common.Interfaces;
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
        private readonly INotificationService _notificationService;

        public RejectBookingService(AddIdentityDbContext context,INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        public async Task<BaseResponse<RejectBookingResponse>> RejectBookingAsync(
            Guid bookingId, string? rejectionReason, CancellationToken ct)
        {
           
            var booking = await _context.PublicBookings
                .Include(b => b.Doctor)
                    .ThenInclude(d => d.User)
                .Include(b => b.Schedule)
                .FirstOrDefaultAsync(b => b.Id == bookingId, ct);

            if (booking == null)
            {
                return ResponseFactory.Fail<RejectBookingResponse>("Booking not found",
                    new List<string> { "The provided bookingId does not match any existing booking." });
            }

            if (booking.Status != Status.Pending)
            {
                return ResponseFactory.Fail<RejectBookingResponse>(
                    $"Booking has already been {booking.Status}",
                    new List<string> { "Only pending bookings can be rejected." });
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
                string title = "Booking Declined"; // تعريف المتغير الذي كان ينقصك
                string message = $"Your booking request with Dr. {booking.Doctor.User.FirstName} {booking.Doctor.User.LastName} " +
                                 $"on {booking.PreferredDate:yyyy-MM-dd} has been rejected.";

                if (!string.IsNullOrWhiteSpace(rejectionReason))
                {
                    message += $" Reason: {rejectionReason}";
                }

                await _notificationService.SendNotificationAsync(
                    recipientId: patientUser.Id,
                    title: title,
                    message: message,
                    type: NotificationType.BookingRejected,
                    relatedId: null // لا توجد جلسة مرتبطة هنا لأن الحجز رُفض
                );
            }

            await _context.SaveChangesAsync(ct);

            // 5. Return response
            return ResponseFactory.Success(new RejectBookingResponse
            {
                Id = booking.Id,
                Status = booking.Status,
                RejectionReason = booking.RejectionReason = "Booking rejected successfully and patient has been notified."
            }, "Booking rejected successfully");
        }
    }
}
