using Application.Common.Interfaces.HR.Recruitment;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Application.Queries.HR.Recruitment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Recruitment
{
    public class GetAllRecruitmentHandler:IRequestHandler<GetRecruitmentQuery, BaseResponse<List<RecruitmentResponse>>>
    {
        private readonly IGetAllRecruitment _getAllRecruitment;

        public GetAllRecruitmentHandler(IGetAllRecruitment getAllRecruitment)
        {
            _getAllRecruitment = getAllRecruitment;
        }

        public async Task<BaseResponse<List<RecruitmentResponse>>> Handle(GetRecruitmentQuery request, CancellationToken ct)
        {
            return await _getAllRecruitment.GetAllRecruitment(ct);
        }
    }
}