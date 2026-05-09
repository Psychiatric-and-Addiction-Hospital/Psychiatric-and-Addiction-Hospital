using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.HR.Department
{
    public record CreateDepartmentCommand(Guid MangerId, string Name, string Description)
        : IRequest<BaseResponse<DepertmentResponse>>;
}
