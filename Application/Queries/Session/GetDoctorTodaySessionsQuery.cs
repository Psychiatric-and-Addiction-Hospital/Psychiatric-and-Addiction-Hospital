using Application.Common.Responses;
using Application.DTOS.Responses.Session;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Session
{
    public record GetDoctorTodaySessionsQuery(string DoctorId)
     : IRequest<BaseResponse<List<SessionShortResponse>>>;
}
