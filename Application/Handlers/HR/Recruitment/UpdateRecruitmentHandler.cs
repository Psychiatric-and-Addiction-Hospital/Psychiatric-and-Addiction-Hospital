using Application.Commands.HR.Recruitment;
using Application.Common.Interfaces.HR.Recruitment;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Recruitment
{
    public class UpdateRecruitmentHandler : IRequestHandler<UpdateRecruitmentCommand, BaseResponse<RecruitmentResponse>>
    {
        private readonly IUpdateRecruitment _updaterecruitment;
        public UpdateRecruitmentHandler(IUpdateRecruitment updaterecruitment)
        {
            _updaterecruitment = updaterecruitment;
        }
        public async Task<BaseResponse<RecruitmentResponse>> Handle(UpdateRecruitmentCommand request,
            CancellationToken ct)
        {
            return await _updaterecruitment.UpdateRecruitmentAsync(request.recruitmentId, request.Title, request.Description, request.MinSalary, request.MaxSalary, request.ExperienceLevel, ct);
        }
    }
}
