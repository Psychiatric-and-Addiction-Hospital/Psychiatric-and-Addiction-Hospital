using Application.Common.Responses;
using Application.DTOS.Request.HR.ApplicationInterview;
using Application.DTOS.Responses.HR.ApplicationInterview;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.HR.ApplicationInterview
{
    public record CompleteInterviewCommand(
        CompleteInterviewRequest Request)
        : IRequest<BaseResponse<ApplicationInterviewResponse>>;
}
