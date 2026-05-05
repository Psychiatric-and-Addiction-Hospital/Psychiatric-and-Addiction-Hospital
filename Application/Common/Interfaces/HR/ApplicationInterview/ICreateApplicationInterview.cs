using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Domain.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.ApplicationInterview
{
    public interface ICreateApplicationInterview
    {
        Task<BaseResponse<ApplicationInterviewResponse>> CreateApplicationInterviewAsync(Guid applicationProcessId, 
            DateTime scheduledTime,string interviewerName, InterviewType interviewType, string location,CancellationToken
            ct);
    }
}
