using Application.Common.Interfaces.HR.Depertment;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Application.DTOS.Responses.HR;
using Domain.Entites.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.services.HR.Depertment
{
    public class GetAllDepertmentService : IGetDepertments
    {
        private readonly AddIdentityDbContext _Context;
        public GetAllDepertmentService(AddIdentityDbContext context)
        {
            _Context = context;
        }

        public async Task<BaseResponse<List<DepartmentResponse>>> GetAllDepertment(CancellationToken ct)
        {
            var departments = await _Context.Departments
          .AsNoTracking()
          .OrderBy(d => d.Name)
          .Select(d => new DepartmentResponse
          {
              Id = d.Id,
              Name = d.Name,
              Description = d.Description!
          })
          .ToListAsync(ct);

            return ResponseFactory.Success(departments,"Departments retrieved successfully.");

        }
    }
}
