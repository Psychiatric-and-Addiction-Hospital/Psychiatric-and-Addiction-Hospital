using Application.Common.Interfaces.HR.Recruitment;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Domain.Entites.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.Recruitment
{
    public class DeleteRecruitmentService : IDeleteRecruitment
    {
        private readonly AddIdentityDbContext _Context;
        public DeleteRecruitmentService(AddIdentityDbContext Context)
        {
            _Context = Context;
        }

        public async Task<BaseResponse<RecruitmentResponse>> DeleteRecruitmentAsync(Guid recruitmentId, CancellationToken ct)
        {

            var recruitment = await _Context.Recruitments.FindAsync(recruitmentId);
            if (recruitment is null)
                return ResponseFactory.Fail<RecruitmentResponse>("Recruitment not found.");
            _Context.Recruitments.Remove(recruitment);
            await _Context.SaveChangesAsync(ct);
            return ResponseFactory.Success<RecruitmentResponse>(null, "Recruitment Deleted Successfully");

        }
    }
}
