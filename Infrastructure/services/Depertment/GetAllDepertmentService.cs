using Application.Common.Interfaces.Depertment;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.services.Depertment
{
    public class GetAllDepertmentService : IGetDepertments
    {
        private readonly AddIdentityDbContext _Context;
        public GetAllDepertmentService(AddIdentityDbContext context)
        {
            _Context = context;
        }

        public async Task<BaseResponse<List<DepertmentResponse>>> GetAllDepertment(CancellationToken ct)
        {
            var Department= await _Context.Departments.Select(d => new DepertmentResponse
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description
            }).ToListAsync(ct);
            if (Department == null || Department.Count == 0)
            {
                return ResponseFactory.Fail<List<DepertmentResponse>>
                   ("No departments found.",
                   new List<string> { "No departments available." });
            }

            return ResponseFactory.Success<List<DepertmentResponse>>
                (Department, "All departments have been fetched successfully");
        }
    }
}
