using Microsoft.EntityFrameworkCore;
using ProjectHK3.Application.Abstractions;
using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Application.Abstractions.IRepositories.Authentication;
using ProjectHK3.Application.Abstractions.IRepositories.Policy_Enrollment___Claims;
using ProjectHK3.Application.Implements;
using ProjectHK3.Application.Settings;
using ProjectHK3.Domain.Abstractions;
using ProjectHK3.Domain.Entities;
using ProjectHK3.Infrastructure.Persistence;
using ProjectHK3.Infrastructure.Repositories.Authentication;
using ProjectHK3.Infrastructure.Repositories.Hospital_Integration;
using ProjectHK3.Infrastructure.Repositories.Insurer___Policy;
using ProjectHK3.Infrastructure.Repositories.Policy_Enrollment___Claims;
using ProjectHK3.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<SmtpSettings>(
    builder.Configuration.GetSection("SmtpSettings")
    );
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAdminLoginRepo, AdminLoginRepo>();
builder.Services.AddScoped<IAuthSessionRepo, AuthSessionRepo>();
builder.Services.AddScoped<IEmpRegisterRepo, EmpRegisterRepo>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IHospitalInfoRepo, HospitalInfoRepo>();
builder.Services.AddScoped<IPolicyRepo, PolicyRepo>();
builder.Services.AddScoped<IPolicyTotalDescriptionRepo, PolicyTotalDescriptionRepo>();
builder.Services.AddScoped<IPolicyRequestDocumentRepo, PolicyRequestDocumentRepo>();

DotNetEnv.Env.Load();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
