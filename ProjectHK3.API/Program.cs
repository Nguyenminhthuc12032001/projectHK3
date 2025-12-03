using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectHK3.Api.Middlewares;
using ProjectHK3.Api.Models;
using ProjectHK3.Application.Abstractions;
using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Application.Abstractions.IRepositories.Authentication;
using ProjectHK3.Application.Abstractions.IRepositories.Policy_Enrollment___Claims;
using ProjectHK3.Application.Implements;
using ProjectHK3.Application.Settings;
using ProjectHK3.Domain.Abstractions;
using ProjectHK3.Infrastructure.Persistence;
using ProjectHK3.Infrastructure.Repositories.Authentication;
using ProjectHK3.Infrastructure.Repositories.Hospital_Integration;
using ProjectHK3.Infrastructure.Repositories.Insurer___Policy;
using ProjectHK3.Infrastructure.Repositories.Policy_Enrollment___Claims;
using ProjectHK3.Infrastructure.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(cfg =>
{
}, typeof(AuthMappingProfile).Assembly);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(typeof(AuthMappingProfile).Assembly);

// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<SmtpSettings>(
    builder.Configuration.GetSection("SmtpSettings")
    );
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
var key = Encoding.ASCII.GetBytes(jwtSettings!.Secret);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAdminLoginRepo, AdminLoginRepo>();
builder.Services.AddScoped<IAuthSessionRepo, AuthSessionRepo>();
builder.Services.AddScoped<IEmpRegisterRepo, EmpRegisterRepo>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IHospitalInfoRepo, HospitalInfoRepo>();
builder.Services.AddScoped<IPolicyRepo, PolicyRepo>();
builder.Services.AddScoped<IPolicyTotalDescriptionRepo, PolicyTotalDescriptionRepo>();
builder.Services.AddScoped<IPolicyRequestDocumentRepo, PolicyRequestDocumentRepo>();
builder.Services.AddScoped<IPasswordResetTokenRepo, PasswordResetTokenRepo>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtSettings.Issuer,

        ValidateAudience = true,
        ValidAudience = jwtSettings.Audience,

        ValidateLifetime = true,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),

        ClockSkew = TimeSpan.Zero
    };
});

DotNetEnv.Env.Load();

// Add Authorization
builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable Auth Middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
