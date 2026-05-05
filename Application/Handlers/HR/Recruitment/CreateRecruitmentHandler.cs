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
    public class CreateRecruitmentHandler : IRequestHandler<CreateRecruitmentCommand, BaseResponse<RecruitmentResponse>>
    {
        private readonly ICreateRecruitment _createRecruitment;
        public CreateRecruitmentHandler(ICreateRecruitment createRecruitment)
        {
             _createRecruitment=  createRecruitment;
        }
        public async Task<BaseResponse<RecruitmentResponse>> Handle(CreateRecruitmentCommand request, CancellationToken ct)
        {
            return await _createRecruitment.CreateRecruitmentAsync(request.Title,request.Description,request.MinSalary,
                request.MaxSalary,request.ExperienceLevel,request.DepartmentId,request.HiringManagerId,ct);
        }
    }
}
