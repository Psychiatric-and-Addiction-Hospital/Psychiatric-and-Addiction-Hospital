using Application.Common.Extensions;
using Application.Common.Interfaces.HR.ApplicationInterview;
using Application.Common.Responses;
using Application.DTOS.Request.HR.ApplicationInterview;
using Application.DTOS.Responses.HR.ApplicationInterview;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.ApplicationInterview
{
    public class GetApplicationInterviewsService : IGetApplicationInterviews
    {
        private readonly AddIdentityDbContext _context;

        public GetApplicationInterviewsService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<PagedResponse<ApplicationInterviewResponse>>> GetAllAsync(
            ApplicationInterviewListRequest request,
            CancellationToken ct)
        {
          
            var query = _context.ApplicationInterviews
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();

                query = query.Where(x =>
                    x.Application.Candidate.FullName.Contains(search) ||
                    x.Application.JobPosting.Title.Contains(search) ||
                    x.Interviewer.FullName.Contains(search));
            }

            if (request.ApplicationId.HasValue)
            {
                query = query.Where(x =>
                    x.ApplicationId == request.ApplicationId.Value);
            }

            if (request.InterviewerId.HasValue)
            {
                query = query.Where(x =>
                    x.InterviewerId == request.InterviewerId.Value);
            }

            if (request.InterviewType.HasValue)
            {
                query = query.Where(x =>
                    x.InterviewType == request.InterviewType.Value);
            }

            if (request.Status.HasValue)
            {
                query = query.Where(x =>
                    x.Status == request.Status.Value);
            }

            if (request.Result.HasValue)
            {
                query = query.Where(x =>
                    x.Result == request.Result.Value);
            }

            if (request.FromDate.HasValue)
            {
                query = query.Where(x =>
                    x.ScheduledAt >= request.FromDate.Value);
            }

            if (request.ToDate.HasValue)
            {
                query = query.Where(x =>
                    x.ScheduledAt <= request.ToDate.Value);
            }

            query = request.SortBy?.ToLower() switch
            {
                "candidate" => request.Descending
                    ? query.OrderByDescending(x => x.Application.Candidate.FullName)
                    : query.OrderBy(x => x.Application.Candidate.FullName),

                "interviewer" => request.Descending
                    ? query.OrderByDescending(x => x.Interviewer.FullName)
                    : query.OrderBy(x => x.Interviewer.FullName),

                "date" => request.Descending
                    ? query.OrderByDescending(x => x.ScheduledAt)
                    : query.OrderBy(x => x.ScheduledAt),

                _ => request.Descending
                    ? query.OrderByDescending(x => x.ScheduledAt)
                    : query.OrderBy(x => x.ScheduledAt)
            };

            var response = query.Select(x => new ApplicationInterviewResponse
            {
                Id = x.Id,

                ApplicationId = x.ApplicationId,

                InterviewerId = x.InterviewerId,

                CandidateName = x.Application.Candidate.FullName,

                JobTitle = x.Application.JobPosting.Title,

                InterviewerName = x.Interviewer.FullName,

                ScheduledAt = x.ScheduledAt,

                DurationInMinutes = x.DurationInMinutes,

                InterviewType = x.InterviewType,

                Status = x.Status,

                Result = x.Result,

                Score = x.Score,

                Location = x.Location,

                MeetingLink = x.MeetingLink,

                Feedback = x.Feedback
            });


            var pagedResult = await response.ToPagedResponseAsync(
                request.PageNumber,
                request.PageSize,
                ct);

            return ResponseFactory.Success(
                pagedResult,
                "Application interviews retrieved successfully.");
        }
    }
}

