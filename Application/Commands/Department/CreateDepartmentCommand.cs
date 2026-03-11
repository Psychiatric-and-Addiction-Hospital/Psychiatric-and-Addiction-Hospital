using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Department
{
    public record CreateDepartmentCommand(string Name, string Description) 
        : IRequest<BaseResponse<DepertmentResponse>>;
}
