using Application.DTO;
using Application.Interfaces;
using Domain.Entites;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DoctorApplicationService : IDoctorApplicationService
    {
        private readonly IGenericRepository<DoctorApplication> _doctorRepo;
        private readonly IUnitOfWork _unitOfWork;

        public DoctorApplicationService(
            IGenericRepository<DoctorApplication> doctorRepo,
            IUnitOfWork unitOfWork)
        {
            _doctorRepo = doctorRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<DoctorApplicationResponseDto> ApplyAsync(DoctorApplicationCreateDto dto)
        {
            // check if email already exists
            var allApps = await _doctorRepo.GetAllAsync();
            if (allApps.Any(d => d.Email == dto.Email))
                throw new Exception("You have already submitted an application.");

            var application = new DoctorApplication
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Specialization = dto.Specialization,
                PhoneNumber = dto.PhoneNumber,
                CVFilePath = dto.CVFilePath,
                Status = Status.Pending,
                AppliedAt = DateTime.Now,
            };

            await _doctorRepo.Add(application);
            await _unitOfWork.CompleteAsync();

            return new DoctorApplicationResponseDto
            {
                FullName = application.FullName,
                Specialization = application.Specialization,
                Status = application.Status.ToString(),
                AppliedAt = application.AppliedAt
            };
        }
    }
}