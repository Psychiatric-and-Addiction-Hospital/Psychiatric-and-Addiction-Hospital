using Application.Common.Interfaces.HR.ApplicationInterview;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Domain.Entites.HR.Applications;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.ApplicationInterview
{
    public class UpdateInterviewResultService : IUpdateInterviewResult
    {
        private readonly AddIdentityDbContext _Context;
        public UpdateInterviewResultService(AddIdentityDbContext context)
        {
            _Context = context;
        }

        public async Task<BaseResponse<ApplicationInterviewResponse>> UpdateInterviewResult(Guid interviewId, string feedback, int score, CancellationToken ct)
        {
            var interviewid = await _Context.ApplicationInterviews.FirstOrDefaultAsync(i => i.Id == interviewId);
            if (interviewid is null)
                return ResponseFactory.Fail<ApplicationInterviewResponse>("Application interview not found.");

            interviewid.Feedback = feedback;
            interviewid.Score = score;

            await _Context.SaveChangesAsync(ct);

            return ResponseFactory.Success<ApplicationInterviewResponse>(null, "Interview result updated successfully");
        }
    }
}
