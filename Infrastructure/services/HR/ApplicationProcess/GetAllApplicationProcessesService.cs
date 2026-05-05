using Application.Common.Interfaces.HR.ApplicationProcess;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Domain.Entites.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.ApplicationProcess
{
    public class GetAllApplicationProcessesService : IGetAllApplicationProcesses
    {
        private readonly AddIdentityDbContext _Context;
        public GetAllApplicationProcessesService(AddIdentityDbContext context)
        {
            _Context = context;
        }

        public async Task<BaseResponse<List<ApplicationProcessResponse>>> GetAllApplicationProcessesAsync(CancellationToken ct)
        {
            var Application = await _Context.ApplicationProcesses.Select(a => new ApplicationProcessResponse
            {
                Id = a.Id,
                CandidateId = a.CandidateId,
                RecruitmentId = a.RecruitmentId,
            }).ToListAsync(ct);

            return ResponseFactory.Success(Application, Application.Any() ? "Department retrieved successfully" : "No Department found");
        }
    }
}