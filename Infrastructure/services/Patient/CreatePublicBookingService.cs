using Application.Common.Interfaces.Patient;
using Application.Common.Responses;
using Application.DTOS.Request.Patient;
using Application.DTOS.Responses;
using Domain.Entites;
using Domain.Enums;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.Patient
{
    public class CreatePublicBookingService : ICreatePublicBooking
    {
        private readonly AddIdentityDbContext _Context;
        public CreatePublicBookingService(AddIdentityDbContext context)
        {
            _Context = context;

        }

        public async Task<BaseResponse<PublicBookingResponse>> CreatePublicBooking(
            CreatePublicBookingRequest request, CancellationToken ct)
        {
            var doctor = await _Context.DoctorProfiles
                .Include(d => d.Employee)
                     .ThenInclude(x => x.AppUser)
                .FirstOrDefaultAsync(d => d.Id == request.doctorId, ct);
            var schedule = await _Context.DoctorSchedules.FirstOrDefaultAsync(s => s.Id == request.ScheduleId,ct);


            if (doctor == null)
                return ResponseFactory.Fail<PublicBookingResponse>("Doctor not found",
                    new List<string> { "The provided doctorId does not match any existing doctor record." });
            if (!doctor.Employee.IsActive)
                return ResponseFactory.Fail<PublicBookingResponse>("Doctor is not available."
                    , new List<string> { "This doctor is currently inactive." });


            if (schedule == null)
                return ResponseFactory.Fail<PublicBookingResponse>("Schedule not found",
                    new List<string> { "The provided ScheduleId does not match any existing Schedule record." });


            if (schedule.DoctorProfileId != request.doctorId)
                return ResponseFactory.Fail<PublicBookingResponse>(
                    "Invalid schedule",
                    new List<string> { "This schedule does not belong to the selected doctor." });

            var scheduledDateTime = schedule.Date.ToDateTime(schedule.Time);

            if (scheduledDateTime <= DateTime.Now)
                return ResponseFactory.Fail<PublicBookingResponse>("Invalid schedule.",
                    new List<string> { "You cannot book a past schedule." });

            var alreadyBooked = await _Context.PublicBookings.AnyAsync(x => x.DoctorId == request.doctorId
            && x.PreferredDate == schedule.Date
            && x.PreferredTime == schedule.Time
            && x.Email == request.email
            && x.Status != Status.Cancelled, ct);

            if (alreadyBooked)
                return ResponseFactory.Fail<PublicBookingResponse>(
                    "Booking already exists.",
                    new List<string> { "You already have a booking for this time slot." });



            if (schedule.IsBooked)
                return ResponseFactory.Fail<PublicBookingResponse>("Slot already booked",
                new List<string> { "The Selected Time Slot Is alresdy booked ." });

            var Booking = new PublicBooking
            {
                FullName = request.fullName,
                PhoneNumber = request.phoneNumber,
                Email = request.email,
                DoctorId = request.doctorId,
                ScheduleId = schedule.Id,
                PreferredDate = schedule.Date,
                PreferredTime = schedule.Time,
                Notes = request.notes,
                Status = Status.Pending
            };
            schedule.IsBooked = true;
            await _Context.PublicBookings.AddAsync(Booking, ct);
            await _Context.SaveChangesAsync(ct);

            return ResponseFactory.Success(new PublicBookingResponse
            {
                Id = Booking.Id,
                FullName = Booking.FullName,
                PhoneNumber = Booking.PhoneNumber,
                Email = Booking.Email,
                DoctorId = Booking.DoctorId,
                DoctorName = doctor.Employee.FirstName,
                PreferredDate = Booking.PreferredDate,
                PreferredTime = Booking.PreferredTime,
                Status = Booking.Status
            }, "Public booking created successfully");
        }
    }
}
