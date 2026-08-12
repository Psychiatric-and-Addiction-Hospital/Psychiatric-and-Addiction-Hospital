using Application.Common.Interfaces.HR.ApplicationInterview;
using Application.Common.Responses;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.ApplicationInterview
{
    public class DeleteApplicationInterviewService: IDeleteApplicationInterview
    {
        private readonly AddIdentityDbContext _context;
        public DeleteApplicationInterviewService(AddIdentityDbContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse<bool>> DeleteAsync(Guid InterviewId, CancellationToken ct)
        {
            var interview = await _context.ApplicationInterviews
                .FirstOrDefaultAsync(x => x.Id == InterviewId, ct);

            if (interview == null)
                return ResponseFactory.Fail<bool>("Interview not found.");

            if (interview.Status == InterviewStatus.Completed)
                return ResponseFactory.Fail<bool>("Completed interviews cannot be deleted.");

            _context.ApplicationInterviews.Remove(interview);

            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(true, "Interview deleted successfully.");

        }
    }
}
