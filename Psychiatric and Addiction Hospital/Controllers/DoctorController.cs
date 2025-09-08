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
    private readonly IDoctorApplicationService _doctorApplicationService;


    public DoctorApplicationsController(
        IDoctorApplicationService doctorApplicationService)
    {
        _doctorApplicationService = doctorApplicationService;
    }

    [HttpPost("join")]
    public async Task<IActionResult> JoinAsDoctor([FromBody] DoctorApplicationCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var response = await _doctorApplicationService.ApplyAsync(dto);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }  
        
}
