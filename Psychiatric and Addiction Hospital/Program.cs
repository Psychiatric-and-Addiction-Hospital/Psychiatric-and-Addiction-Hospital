using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Psychiatric_and_Addiction_Hospital.Extesion;
using Infrastructure.Dependency;
using Application.Dependency;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter 'Bearer' [space] and then your token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// ---------- CORS ----------

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// ---------- Add services to the container ----------
builder.Services.AddApplication();
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddInfrastructureDBContext(builder.Configuration);
builder.Services.AddInfrastructureServices();

// ---------- Controllers ----------
builder.Services.AddControllers();

// ---------- Validation ----------
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    //app.MapOpenApi();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAngularDev");
//app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    options.RoutePrefix = "";
});
app.MapControllers();

app.Run();
