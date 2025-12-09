using Microsoft.EntityFrameworkCore;
using ProjectHK3.Application.Abstractions;
using ProjectHK3.Domain.Entities;
using ProjectHK3.Domain.ValueObjects;
using ProjectHK3.Infrastructure.Persistence;

namespace ProjectHK3.Infrastructure.Seeders
{
    public static class AdminLoginSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context, IPasswordService passwordService)
        {
            if (!await context.AdminLogin.IgnoreQueryFilters().AnyAsync())
            {
                var admin = new AdminLogin
                {
                    UserName = "admin",
                    Email = new EmailAddress("admin@gmail.com"),
                    PasswordHash = passwordService.HashPassword("123456"),
                    Role = RoleOfAdminLogin.Admin,
                    Status = StatusOfAdminLogin.Active,
                    IsDeleted = false
                };

                await context.AdminLogin.AddAsync(admin);
                await context.SaveChangesAsync();
            }
        }
    }
}
