using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.HR.ApplicationOffer;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Domain.Enums;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.services.HR.ApplicationOffer
{
    public class CreateApplicationOfferService : ICreateApplicationOffer
    {
        private readonly AddIdentityDbContext _Context;
        private readonly IEmailService _Email;
        public CreateApplicationOfferService(AddIdentityDbContext context, IEmailService email)
        {
            _Context = context;
            _Email = email;
        }

        public async Task<BaseResponse<ApplicationOfferResponse>> CreateApplicationOfferAsync(Guid applicationProcessId, decimal offeredSalary, DateTime expiryDate, CancellationToken ct)
        {
            var applicationProcess = await _Context.ApplicationProcesses
                .Include(p => p.Candidate)
                .Include(p => p.Recruitment)
                .FirstOrDefaultAsync(ap => ap.Id == applicationProcessId, ct);

            if (applicationProcess is null)
                return ResponseFactory.Fail<ApplicationOfferResponse>("Application process not found.");


            var applicationOffer = new Domain.Entites.HR.Applications.ApplicationOffer
            {
                ApplicationProcessId = applicationProcessId,
                OfferedSalary = offeredSalary,
                ExpiryDate = expiryDate,
                statues = OfferStatues.Pending
            };

            await _Context.ApplicationOffers.AddAsync(applicationOffer, ct);
            await _Context.SaveChangesAsync(ct);

            var candidate = applicationProcess.Candidate;

            string subject = $"Job Offer – {applicationProcess.Recruitment.Title}";
            string body = $@"
             Hello {candidate.FullName},
             Congratulations!
             We are pleased to inform you that you have been selected for the position **{applicationProcess.Recruitment.Title}**.
             Here are the details of your job offer:
             Position**: {applicationProcess.Recruitment.Title}
             Offered Salary**: {offeredSalary} EGP
             Offer Expiry Date**: {expiryDate:yyyy-MM-dd}
             Please review the offer and respond before the expiry date.
             We look forward to welcoming you to the team!Best regards,HR Team";

            await _Email.SendAsync(
                to: candidate.Email,
                subject: subject,
                body: body
            );

            return ResponseFactory.Success(new ApplicationOfferResponse
            {
                Id = applicationOffer.Id,
                ApplicationProcessId = applicationOffer.ApplicationProcessId,
                OfferedSalary = applicationOffer.OfferedSalary,
                ExpiryDate = applicationOffer.ExpiryDate,
                Statues = applicationOffer.statues
            },
            "Application offer created and email sent successfully.");
        }
    }
}
