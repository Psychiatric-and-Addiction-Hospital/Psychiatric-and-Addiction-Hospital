using Application.DTO;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.Entites;
using Domain.Enums;
using Domain.Interfaces;
using FluentValidation.Validators;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

[ApiController]
[Route("api/[controller]")]
public class DoctorApplicationsController : ControllerBase
{
    private readonly AddIdentityDbContext _Context;
    private readonly UserManager<AppUser> _UserManager;
    private readonly IAuthService _AuthService;
    private readonly IMapper _Mapper;
    private readonly IEmailService _EmailService;
    private readonly ITokenService _TokenService;
    private readonly IPasswordService _PasswordService;
    private readonly IPhoneService _PhoneService;
    private readonly ISpecializationService _SpecializationsService;
    private readonly INameService _NameService;
    private readonly ICVFilePathValidationService _CVFileValidationService;


    public DoctorApplicationsController(
        AddIdentityDbContext Context,
        UserManager<AppUser> UserManager,
        IAuthService AuthService,
        IMapper Mapper,
        IEmailService EmailService,
        ITokenService TokenService,
        IPasswordService PasswordService,
        IPhoneService PhoneService,
        ISpecializationService SpecializationsService,
        INameService NameService,
        ICVFilePathValidationService CVFileValidationService)
    {
        _Context = Context;
        _UserManager = UserManager;
        _AuthService = AuthService;
        _Mapper = Mapper;
        _EmailService = EmailService;
        _TokenService = TokenService;
        _PasswordService = PasswordService;
        _PhoneService = PhoneService;
        _SpecializationsService = SpecializationsService;
        _NameService = NameService;
        _CVFileValidationService = CVFileValidationService;
    }

    [HttpPost("join")]
    public async Task<IActionResult> JoinAsDoctor([FromBody] DoctorApplicationCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // check if the email is already used for an application
        bool alreadyApplied = await _Context.DoctorApplications
            .AnyAsync(d => d.Email == dto.Email);

        // check for already applied
        if (alreadyApplied)
            return BadRequest("You have already submitted an application.");

        // check for full name validity
        if (!_NameService.IsValidName(dto.FullName))
            return BadRequest("Invalid full name. name should be bigger than 2 characters and only letters and spaces are allowed.");

        // check for email validity
        if (!_EmailService.IsValidEmail(dto.Email))
            return BadRequest("Invalid email format.");

        // check for specialization validity
        if (!_SpecializationsService.IsValidSpecialization(dto.Specialization))
            return BadRequest("Specialization is not valid.");

        // check for phone number validity
        if (!_PhoneService.PhoneValid(dto.PhoneNumber))
            return BadRequest("Invalid phone number format. Must be in international format (e.g. +1234567890)");

        // check for cv format validity
        if (!_CVFileValidationService.IsValidCV(dto.CVFilePath))
            return BadRequest("Invalid CV file. Only PDF/DOC/DOCX are allowed, max size 5MB.");

        // check for password strength
        if (!_PasswordService.IsValidPassword(dto.Password))
            return BadRequest("Password must be at least 8 characters long, contain an uppercase letter, a lowercase letter, a number, and a special character.");

        // new user creation
        var user = new AppUser
        {
            UserName = dto.Email,
            Email = dto.Email
        };

        var result = await _UserManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        // doctor profile creation
        var application = new DoctorApplication
        {
            FullName = dto.FullName,
            Email = dto.Email,
            Specialization = dto.Specialization,
            PhoneNumber = dto.PhoneNumber,
            CVFilePath = dto.CVFilePath,
            Status = Status.Pending,
            AppliedAt = DateTime.Now,
            UserId = user.Id
        };

        _Context.DoctorApplications.Add(application);
        await _Context.SaveChangesAsync();

        // token creation
        var tokens = await _AuthService.CreateTokenWithRefresh(user);

        // Response
        var response = new
        {
            Application = new DoctorApplicationResponseDto
            {
                Id = application.Id,
                FullName = application.FullName,
                Specialization = application.Specialization,
                Status = application.Status.ToString(),
                AppliedAt = application.AppliedAt
            },
            AccessToken = tokens.AccessToken,
            RefreshToken = tokens.RefreshToken
        };

        return Ok(response);
    }

  

}
