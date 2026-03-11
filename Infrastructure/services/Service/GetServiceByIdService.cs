using Application.Common.Interfaces.Services;
using Application.Common.Responses;
using Application.DTOS.Responses;
using FluentResults;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.Service
{
    public class GetServiceByIdService : IGetServiceById
    {
        private readonly AddIdentityDbContext _context;

        public GetServiceByIdService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<ServiceResponse>> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var serviceid = await _context.Services
                .Where(s => s.Id == id)
                .Select(s => new ServiceResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    DepartmentName = s.Department.Name
                })
                .FirstOrDefaultAsync(ct);

            if (serviceid == null)
                return ResponseFactory.Fail<ServiceResponse>("Service not found.");

            return ResponseFactory.Success(serviceid, "Service retrieved successfully.");
        }
    }

}
