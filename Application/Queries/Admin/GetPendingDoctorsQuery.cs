using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Admin
{
    public record GetPendingDoctorsQuery():IRequest<BaseResponse<List<PendingDoctorApplicationResponse>>>;

}
