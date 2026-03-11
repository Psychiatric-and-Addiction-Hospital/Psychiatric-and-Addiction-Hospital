using Application.Common.Interfaces.Services;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.Service
{
    public class GetAllServicesService : IGetAllServices
    {
        private readonly AddIdentityDbContext _context;

        public GetAllServicesService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<List<ServiceResponse>>> GetAllAsync(CancellationToken ct)
        {
            var AllService= await _context.Services
                .Select(s => new ServiceResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    DepartmentName = s.Department.Name
                })
                .ToListAsync(ct);
            if (AllService == null || AllService.Count == 0)
            {
                return ResponseFactory.Fail<List<ServiceResponse>>("No services found.");
            }

            return ResponseFactory.Success(AllService, "All services have been fetched successfully.");
        }
    }}

