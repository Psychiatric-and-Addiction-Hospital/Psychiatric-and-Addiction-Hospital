using Application.Common.Interfaces.Services;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.services.Service
{
    public class DeleteServiceService : IDeleteService
    {
        private readonly AddIdentityDbContext _Context;

        public DeleteServiceService(AddIdentityDbContext context)
        {
            _Context = context;
        }

        public async Task<BaseResponse<ServiceResponse>> DeleteAsync(Guid Id, CancellationToken ct)
        {
            var service = await _Context.Services
                .FirstOrDefaultAsync(d => d.Id == Id, ct);

            if (service == null)
                return ResponseFactory.Fail<ServiceResponse>("Service not found.");

            _Context.Services.Remove(service);
            await _Context.SaveChangesAsync(ct);

            return ResponseFactory.Success<ServiceResponse>(null,"Service Deleted Successfully");
        }
    }
}


