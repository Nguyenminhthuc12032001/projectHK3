using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectHK3.Api.Converters;
using ProjectHK3.Api.Middlewares;
using ProjectHK3.Api.Models;
using ProjectHK3.Application.Abstractions;
using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Application.Abstractions.IRepositories.Authentication;
using ProjectHK3.Application.Abstractions.IRepositories.Policy_Enrollment___Claims;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.Implements;
using ProjectHK3.Application.Implements.Services;
using ProjectHK3.Application.Settings;
using ProjectHK3.Domain.Abstractions;
using ProjectHK3.Infrastructure.Persistence;
using ProjectHK3.Infrastructure.Repositories.Authentication;
using ProjectHK3.Infrastructure.Repositories.Finance___Reporting;
using ProjectHK3.Infrastructure.Repositories.Hospital_Integration;
using ProjectHK3.Infrastructure.Repositories.Insurer___Policy;
using ProjectHK3.Infrastructure.Repositories.Notification___Audit;
using ProjectHK3.Infrastructure.Repositories.Policy_Enrollment___Claims;
using ProjectHK3.Infrastructure.Services;
using System.Text;
using ProjectHK3.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(cfg =>
{
}, typeof(AuthMappingProfile).Assembly);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(typeof(AuthMappingProfile).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<SmtpSettings>(
    builder.Configuration.GetSection("SmtpSettings")
    );
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
var key = Encoding.UTF8.GetBytes(jwtSettings!.Secret);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Service
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
//builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IFinanceService, FinanceService>();
builder.Services.AddScoped<IHospitalService, HospitalService>();
builder.Services.AddScoped<IInsurerService, InsurerService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IPolicyApprovalService, PolicyApprovalService>();
builder.Services.AddScoped<IPolicyDescriptionService, PolicyDescriptionService>();
builder.Services.AddScoped<IPolicyEmployeeService, PolicyEmployeeService>();
builder.Services.AddScoped<IPolicyRequestService, PolicyRequestService>();
builder.Services.AddScoped<IPolicyService, PolicyService>();
builder.Services.AddScoped<IReportService, ReportService>();


//Repo
builder.Services.AddScoped<IHospitalInfoRepo, HospitalInfoRepo>();
builder.Services.AddScoped<ICompanyDetailRepo, CompanyDetailRepo>();
builder.Services.AddScoped<IPolicyRepo, PolicyRepo>();
builder.Services.AddScoped<IPolicyTotalDescriptionRepo, PolicyTotalDescriptionRepo>();
builder.Services.AddScoped<IPolicyRequestDocumentRepo, PolicyRequestDocumentRepo>();
builder.Services.AddScoped<IPasswordResetTokenRepo, PasswordResetTokenRepo>();
builder.Services.AddScoped<IAdminLoginRepo, AdminLoginRepo>();
builder.Services.AddScoped<IAuthSessionRepo, AuthSessionRepo>();
builder.Services.AddScoped<IEmpRegisterRepo, EmpRegisterRepo>();
builder.Services.AddScoped<IReportLogRepo, ReportLogRepo>();
builder.Services.AddScoped<ITransactionLedgerRepo, TransactionLedgerRepo>();
builder.Services.AddScoped<IAuditTrailRepo, AuditTrailRepo>();
builder.Services.AddScoped<INotificationLogRepo, NotificationLogRepo>();
builder.Services.AddScoped<IPoliciesOnEmployeeRepo, PoliciesOnEmployeeRepo>();
builder.Services.AddScoped<IPolicyApprovalDetailRepo, PolicyApprovalDetailRepo>();
builder.Services.AddScoped<IPolicyRequestDetailRepo, PolicyRequestDetailRepo>();


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
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.AddAuthorization();
builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await db.Database.MigrateAsync();
    var passwordService = scope.ServiceProvider.GetRequiredService<IPasswordService>();
    await AdminLoginSeeder.SeedAsync(db, passwordService);
}


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
app.UseCors("AllowFrontend");

app.MapControllers();

app.Run();
