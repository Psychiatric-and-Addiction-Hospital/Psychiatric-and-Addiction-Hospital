using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.HR.ApplicationProcesses
{
    public record GetAllApplicationProcessesQuery (): IRequest<BaseResponse<List<ApplicationProcessResponse>>>;
  
}
