using Application.Common.Interfaces.HR.ApplicationProcess;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Domain.Enums;
using Infrastructure.Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.ApplicationProcess
{
    public class UpdateApplicationProcessStatusService : IUpdateApplicationProcessStatus
    {
        private readonly AddIdentityDbContext _Context;
        public UpdateApplicationProcessStatusService(AddIdentityDbContext context)
        {
            _Context = context;
        }
        public async Task<BaseResponse<ApplicationProcessResponse>> UpdateApplicationProcessStatus(Guid applicationProcessId, ApplicationStatus status, CancellationToken ct)
        {
            var UpdateApplicationProcess = await _Context.ApplicationProcesses.FindAsync(applicationProcessId);
            if (UpdateApplicationProcess == null)
                return ResponseFactory.Fail<ApplicationProcessResponse>("Application Process Not Found");

            UpdateApplicationProcess.States = status;
            await _Context.SaveChangesAsync(ct);

            return ResponseFactory.Success<ApplicationProcessResponse>(null, "Application Process Status Updated Successfully");
        }
    }
}