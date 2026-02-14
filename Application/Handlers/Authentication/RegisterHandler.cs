using Application.Commands.Authentication;
using Application.Common.Interfaces.Authentication;
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
    public class RegisterHandler : IRequestHandler<RegisterCommand, Result<string>>
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
        public async Task<Result<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Register process started for {Email}", request.Email);
            if (request.Password != request.ConfirmPassword)
            {
                return Result.Fail("Password and ConfirmPassword do not match");
            }

            var user = new AppUser
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                RoleType = RoleType.Patient,
                LastName = request.LastName,
                PhoneNumber=request.PhoneNumber,
                Addres=request.Addres,

            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogWarning("Register failed: {Errors}", errors);
                return Result.Fail(errors);
            }

            var token = await _jwtGenerator.GenerateTokenAsync(user);

            return Result.Ok(token);
        }

    }
}

