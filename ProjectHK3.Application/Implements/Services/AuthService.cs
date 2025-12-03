using ProjectHK3.Application.Abstractions;
using ProjectHK3.Application.Abstractions.IRepositories.Authentication;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.Auth;
using ProjectHK3.Domain.Abstractions;
using ProjectHK3.Domain.Emtitys;
using ProjectHK3.Domain.Entities;
using ProjectHK3.Domain.ValueObjects;

namespace ProjectHK3.Application.Implements.Services
{
    public class AuthService(
        IUnitOfWork unitOfWork,
        IAdminLoginRepo adminLoginRepo,
        IAuthSessionRepo authSessionRepo,
        IEmpRegisterRepo empRegisterRepo,
        ICurrentUserService currentUserService,
        ITokenService tokenService,
        IPasswordService passwordService,
        IEmailSender emailSender,
        IPasswordResetTokenRepo passwordResetTokenRepo
        ) : IAuthService
    {
        readonly IUnitOfWork _unitOfWork = unitOfWork;
        readonly IAdminLoginRepo _adminLoginRepo = adminLoginRepo;
        readonly IAuthSessionRepo _authSessionRepo = authSessionRepo;
        readonly IEmpRegisterRepo _employeeRegisterRepo = empRegisterRepo;
        readonly ICurrentUserService _currentUserService = currentUserService;
        readonly ITokenService _tokenService = tokenService;
        readonly IPasswordService _passwordService = passwordService;
        readonly IEmailSender _emailSender = emailSender;
        readonly IPasswordResetTokenRepo _passwordResetTokenRepo = passwordResetTokenRepo;

        public async Task<bool> AddNewAdminAsync(NewAdminRequest request)
        {
            var admins = await _adminLoginRepo.GetAllAsync();
            bool isExistEmail = admins.Any(a => a.Email == new EmailAddress(request.Email));
            if (isExistEmail) return false;
            var newAdmin = new AdminLogin
            {
                UserName = request.UserName,
                Email = new EmailAddress(request.Email),
                PasswordHash = _passwordService.HashPassword(request.Password), 
                Role = request.Role switch
                {
                    "Admin" => RoleOfAdminLogin.Admin,
                    "Manager" => RoleOfAdminLogin.Manager,
                    "Finance" => RoleOfAdminLogin.Finance,
                    _ => RoleOfAdminLogin.Admin,
                }
            };
            await _adminLoginRepo.AddOneAsync(newAdmin);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangePasswordAdminAsync(ChangePasswordRequest.Admin request)
        {
            var match = await _adminLoginRepo.GetOneAsync(request.AdminId);
            if (match is null) return false;
            if (match.PasswordHash != request.CurrentPassword) return false;
            match.PasswordHash = request.NewPassword;
            await _adminLoginRepo.UpdateOneAsync(match);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ChangePasswordEmployeeAsync(ChangePasswordRequest.Employee request)
        {
            var match = await _employeeRegisterRepo.GetOneAsync(request.EmployeeId);
            if (match is null) return false;
            if (match.PasswordHash != request.CurrentPassword) return false;
            match.PasswordHash = request.NewPassword;
            await _employeeRegisterRepo.UpdateOneAsync(match);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ForgotPasswordAsync(string email)
        {
            var admins = await _adminLoginRepo.GetAllAsync();
            var matchAdmin = admins.FirstOrDefault(u => u.Email == new EmailAddress(email) && u.IsDeleted == false);
            if (matchAdmin != null)
            {
                var token = Guid.NewGuid().ToString();
                var resetLink = $"https://yourapp.com/reset-password?token={token}";
                
                var oldTokens = await _passwordResetTokenRepo.GetAllAsync();
                var oldTokenMatch = oldTokens.FirstOrDefault(t => t.AdminId == matchAdmin.Id && !t.Used && t.ExpiresAt > DateTime.UtcNow && t.IsDeleted == false && t.TokenType == TokenType.PasswordReset);
                if (oldTokenMatch != null)
                {
                    oldTokenMatch.Used = true;
                    await _passwordResetTokenRepo.UpdateOneAsync(oldTokenMatch);
                }

                await _passwordResetTokenRepo.AddOneAsync(new PasswordResetToken
                {
                    AdminId = matchAdmin.Id,
                    HashedToken = _tokenService.HashToken(token),
                    TokenType = TokenType.PasswordReset
                });
                await _unitOfWork.SaveChangesAsync();
                await _emailSender.SendEmailAsync(
                    email,
                    "Password Reset Request",
                    $"Click the link to reset your password: {resetLink}"
                );
                return true;
            }

            var employees = await _employeeRegisterRepo.GetAllAsync();
            var matchEmployee = employees.FirstOrDefault(u => u.Email == new EmailAddress(email) && u.IsDeleted == false);
            if (matchEmployee != null)
            {
                var token = Guid.NewGuid().ToString();
                var resetLink = $"https://yourapp.com/reset-password?token={token}";

                var oldTokens = await _passwordResetTokenRepo.GetAllAsync();
                var oldTokenMatch = oldTokens.FirstOrDefault(t => t.EmpId == matchEmployee.Id && !t.Used && t.ExpiresAt > DateTime.UtcNow && t.IsDeleted == false && t.TokenType == TokenType.PasswordReset);
                if (oldTokenMatch != null)
                {
                    oldTokenMatch.Used = true;
                    await _passwordResetTokenRepo.UpdateOneAsync(oldTokenMatch);
                }
                await _passwordResetTokenRepo.AddOneAsync(new PasswordResetToken
                {
                    EmpId = matchEmployee.Id,
                    HashedToken = _tokenService.HashToken(token),
                    TokenType = TokenType.PasswordReset
                });
                await _unitOfWork.SaveChangesAsync();
                await _emailSender.SendEmailAsync(
                    email,
                    "Password Reset Request",
                    $"Click the link to reset your password: {resetLink}"
                );

                return true;
            }

            return false;
        }

        public async Task<UserInfoDto?> GetCurrentUserAsync()
        {
            var currentUserEmail = _currentUserService.Email;
            if (currentUserEmail == null) return null;
            var currentUserRole = _currentUserService.Role;
            if (currentUserRole == null) return null;
            if (currentUserRole == "Admin" || currentUserRole == "Manager" || currentUserRole == "Finace")
            {
                var users = await _adminLoginRepo.GetAllAsync();
                var match = users.FirstOrDefault(u => u.Email == new EmailAddress(currentUserEmail) && u.IsDeleted == false);
                if (match == null) return null;
                return new UserInfoDto
                {
                    Id = match.Id,
                    Email = match.Email?.ToString() ?? "",
                    Name = match.UserName ?? "",
                    Role = match.Role.ToString(),
                };
            }
            else if (currentUserRole == "Employee")
            {
                var users = await _employeeRegisterRepo.GetAllAsync();
                var match = users.FirstOrDefault(u => u.Email == new EmailAddress(currentUserEmail) && u.IsDeleted == false);
                if (match == null) return null;
                return new UserInfoDto
                {
                    Id = match.Id,
                    Email = match.Email?.ToString() ?? "",
                    Name = match.FullName ?? "",
                    Phone = match.Phone?.ToString() ?? "",
                    Role = "Employee",
                };
            }
            return null;
        }

        public async Task<AuthResponse?> LoginAsync(LoginRequest request)
        {
            var admins = await _adminLoginRepo.GetAllAsync();
            var employees = await _employeeRegisterRepo.GetAllAsync();
            var matchAdmin = admins.FirstOrDefault(a => a.Email == new EmailAddress(request.Email) && a.IsDeleted == false && a.Status == StatusOfAdminLogin.Active);
            var matchEmployee = employees.FirstOrDefault(e => e.Email == new EmailAddress(request.Email) && e.IsDeleted == false && e.Status == StatusOfEmpRegister.Active);
            if (matchAdmin == null && matchEmployee == null)
            {
                return null;
            }
            AuthSession authSession = new();
            var reFreshToken = _tokenService.GenerateRefreshToken();
            if (matchAdmin != null)
            {
                var result = _passwordService.VerifyPassword(matchAdmin.PasswordHash!, request.Password!);
                if (!result) return null;
                var accessToken = _tokenService.GenerateAccessToken(matchAdmin.Id, matchAdmin.Email!.ToString());
                authSession.AdminId = matchAdmin.Id;
                authSession.RefreshToken = reFreshToken;
                authSession.Role = matchAdmin.Role switch
                {
                    RoleOfAdminLogin.Admin => RoleOfAuthSession.Admin,
                    RoleOfAdminLogin.Manager => RoleOfAuthSession.Manager,
                    RoleOfAdminLogin.Finance => RoleOfAuthSession.Finance,
                    _ => RoleOfAuthSession.Admin,
                };
                await _authSessionRepo.AddOneAsync(authSession);
                await _unitOfWork.SaveChangesAsync();
                return new AuthResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = reFreshToken,
                    ExpireAt = authSession.ExpiresAt,
                };
            }
            else if (matchEmployee != null)
            {
                var result = _passwordService.VerifyPassword(matchEmployee.PasswordHash!, request.Password!);
                if (! result) return null;
                var accessToken = _tokenService.GenerateAccessToken(matchEmployee.Id, matchEmployee.Email!.ToString());
                authSession.EmployeeId = matchEmployee.Id;
                authSession.RefreshToken = reFreshToken;
                authSession.Role = RoleOfAuthSession.Employee;
                await _authSessionRepo.AddOneAsync(authSession);
                await _unitOfWork.SaveChangesAsync();
                return new AuthResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = reFreshToken,
                    ExpireAt = authSession.ExpiresAt,
                };
            }
            return null;
        }

        public async Task<bool> LogOutAsync(string refreshToken)
        {
            var authSessions = await _authSessionRepo.GetAllAsync();
            var match = authSessions.FirstOrDefault(a => a.RefreshToken == refreshToken && a.IsDeleted == false);
            if (match == null) return false;
            await _authSessionRepo.DeleteOneAsync(match.Id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            var employees = await _employeeRegisterRepo.GetAllAsync();
            var isExistEmail = employees.Any(e => e.Email == new EmailAddress(request.Email) && e.IsDeleted == false);
            if (isExistEmail) return false;
            var isExistPhone = employees.Any(e => e.Phone == new PhoneNumber(request.Phone) && e.IsDeleted == false);
            if (isExistPhone) return false;
            var newEmployee = new EmpRegister
            {
                FullName = request.FullName,
                Email = new EmailAddress(request.Email),
                Phone = new PhoneNumber(request.Phone),
                PasswordHash = _passwordService.HashPassword(request.Password),
                Department = request.Department,
                HireDate = DateOnly.Parse(request.HireDate),
                CompanyId = request.CompanyId,
                Status = StatusOfEmpRegister.Inactive,
            };
            await _employeeRegisterRepo.AddOneAsync(newEmployee);
            await _unitOfWork.SaveChangesAsync();

            var token = Guid.NewGuid().ToString();
            var verifyLink = $"https://yourapp.com/verify-account?token={token}";

            var oldTokens = await _passwordResetTokenRepo.GetAllAsync();
            var oldTokenMatch = oldTokens.FirstOrDefault(t => t.EmpId == newEmployee.Id && !t.Used && t.ExpiresAt > DateTime.UtcNow && t.IsDeleted == false && t.TokenType == TokenType.EmailVerification);
            if (oldTokenMatch != null)
            {
                oldTokenMatch.Used = true;
                oldTokenMatch.IsDeleted = true;
                await _passwordResetTokenRepo.UpdateOneAsync(oldTokenMatch);
            }
            await _passwordResetTokenRepo.AddOneAsync(new PasswordResetToken
            {
                EmpId = newEmployee.Id,
                HashedToken = _tokenService.HashToken(token),
                TokenType = TokenType.EmailVerification
            });
            await _unitOfWork.SaveChangesAsync();
            await _emailSender.SendEmailAsync(
                newEmployee.Email.ToString(),
                "Verify Account Request",
                $"Click the link to verify your account: {verifyLink}"
            );
            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var tokens = await _passwordResetTokenRepo.GetAllAsync();
            var match = tokens.FirstOrDefault(t => t.HashedToken == _tokenService.HashToken(request.Token!) && !t.Used && t.ExpiresAt > DateTime.UtcNow && t.IsDeleted == false && t.TokenType == TokenType.PasswordReset);
            if (match == null) return false;
            if (match.AdminId != 0)
            {
                var admin = await _adminLoginRepo.GetOneAsync(match.AdminId);
                if (admin == null) return false;
                admin.PasswordHash = _passwordService.HashPassword(request.NewPassword!);
                match.Used = true;
                await _adminLoginRepo.UpdateOneAsync(admin);
                await _passwordResetTokenRepo.UpdateOneAsync(match);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            else if (match.EmpId != 0)
            {
                var employee = await _employeeRegisterRepo.GetOneAsync(match.EmpId);
                if (employee == null) return false;
                employee.PasswordHash = _passwordService.HashPassword(request.NewPassword!);
                match.Used = true;
                await _employeeRegisterRepo.UpdateOneAsync(employee);
                await _passwordResetTokenRepo.UpdateOneAsync(match);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<AuthValidateResponse?> ValidateTokenAsync(string token)
        {
            var all = await _passwordResetTokenRepo.GetAllAsync();

            var match = all.FirstOrDefault(a => a.HashedToken == _tokenService.HashToken(token) && a.TokenType == TokenType.EmailVerification && a.ExpiresAt >= DateTime.UtcNow && a.IsDeleted == false && a.Used == false);
            if (match == null) return null;
            match.Used = true;

            var empMatch = await _employeeRegisterRepo.GetOneAsync(match.EmpId);
            if (empMatch == null) return null;
            empMatch.Status = StatusOfEmpRegister.Active;

            await _unitOfWork.SaveChangesAsync();

            return new AuthValidateResponse
            {
                EmpId = empMatch.Id,
                Name = empMatch.FullName ?? string.Empty,
                Email = empMatch.Email!.ToString(),
                Status = empMatch.Status.ToString(),
            };
        }
    }
}
