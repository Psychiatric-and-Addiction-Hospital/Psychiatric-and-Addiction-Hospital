using Application.Commands.Authentication;
using Application.Common.Interfaces.Authentication;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Domain.Entites;
using Domain.Enums;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Authentication
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, BaseResponse<RegisterResponse>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly ILogger<RegisterHandler> _logger;

        public RegisterHandler(
            UserManager<AppUser> userManager,
            IJwtGenerator jwtGenerator,
            ILogger<RegisterHandler> logger)
        {
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
            _logger = logger;
        }
        public async Task<BaseResponse<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Register process started for {Email}", request.Email);
            if (request.Password != request.ConfirmPassword)
            {
                return ResponseFactory.Fail<RegisterResponse>("Password and ConfirmPassword do not matchPassword and ConfirmPassword do not match");
                
            }

            var user = new AppUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email,
                Email = request.Email,
                Gender = request.gender,
                PhoneNumber = request.PhoneNumber,
                Addres = request.Addres,
                RoleType = RoleType.Patient,
                
             

            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return ResponseFactory.Fail<RegisterResponse>("Registration failed", errors);
            }

            var token = await _jwtGenerator.GenerateTokenAsync(user);

            

            var dto = new RegisterResponse
            {
                Message = "Registration successful",
                AccessToken = token
            };

            return ResponseFactory.Success(dto);

        }

    }
}

