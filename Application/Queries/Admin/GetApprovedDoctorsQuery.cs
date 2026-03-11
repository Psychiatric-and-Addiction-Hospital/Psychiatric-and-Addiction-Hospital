using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Admin
{
    public record GetApprovedDoctorsQuery() : IRequest<BaseResponse<List<ApprovedDoctorApplicationRespons>>>;

}
