using Microsoft.Extensions.Logging;
using ProjectHK3.Application.Abstractions;
using ProjectHK3.Application.Abstractions.IRepositories.Authentication;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.Auth;
using ProjectHK3.Application.Exceptions;
using ProjectHK3.Domain.Abstractions;
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
        IPasswordResetTokenRepo passwordResetTokenRepo,
        ILogger<AuthService> logger
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
        readonly ILogger<AuthService> _logger = logger;

        public async Task<bool> AddNewAdminAsync(NewAdminRequest request)
        {
            try
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
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, "[AddnewAsync] BUSINESS ERROR: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[AddnewAsync] ERROR: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<bool> ChangePasswordAdminAsync(ChangePasswordRequest.Admin request)
        {
            try
            {
                var match = await _adminLoginRepo.GetOneAsync(request.AdminId) ?? throw new BusinessException("Admin not found", 404);

                if (!_passwordService.VerifyPassword(match.PasswordHash!, request.CurrentPassword))
                    throw new BusinessException("Current password is incorrect.", 400);

                if (_passwordService.VerifyPassword(match.PasswordHash!, request.NewPassword))
                    throw new BusinessException("New password must be different.", 400);

                match.PasswordHash = _passwordService.HashPassword(request.NewPassword);

                await _adminLoginRepo.UpdateOneAsync(match);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, "[ChangePasswordAdminAsync] BUSINESS ERROR: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[ChangePasswordAdminAsync] ERROR: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<bool> ChangePasswordEmployeeAsync(ChangePasswordRequest.Employee request)
        {
            try
            {
                var match = await _employeeRegisterRepo.GetOneAsync(request.EmployeeId) ?? throw new BusinessException("Emp not found", 404);

                if (!_passwordService.VerifyPassword(match.PasswordHash!, request.CurrentPassword))
                    throw new BusinessException("Current password is incorrect.", 400);

                if (_passwordService.VerifyPassword(match.PasswordHash!, request.NewPassword))
                    throw new BusinessException("New password must be different.", 400);

                match.PasswordHash = _passwordService.HashPassword(request.NewPassword);

                await _employeeRegisterRepo.UpdateOneAsync(match);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, "[ChangePasswordEmpAsync] BUSINESS ERROR: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[ChangePasswordEmpAsync] ERROR: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<bool> ForgotPasswordAsync(string email)
        {
            try
            {
                var admins = await _adminLoginRepo.GetAllAsync();
                var matchAdmin = admins.FirstOrDefault(u => u.Email == new EmailAddress(email) && u.IsDeleted == false && u.Status == StatusOfAdminLogin.Active);
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
                var matchEmployee = employees.FirstOrDefault(u => u.Email == new EmailAddress(email) && u.IsDeleted == false && u.Status == StatusOfEmpRegister.Active);
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
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, "[ForgotPasswordAsync] BUSINESS ERROR: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[ForgotPasswordAsync] ERROR: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<UserInfoDto?> GetCurrentUserAsync()
        {
            try
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
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, "[GetUserAsync] BUSINESS ERROR: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[GetUserAsync] ERROR: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<AuthResponse?> LoginAsync(LoginRequest request)
        {
            try
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
                    var accessToken = _tokenService.GenerateAccessToken(matchAdmin.Id, matchAdmin.Email!.ToString(), matchAdmin.Role.ToString());
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
                    if (!result) return null;
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
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, "[LoginAsync] BUSINESS ERROR: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[LoginAsync] ERROR: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<bool> LogOutAsync(string refreshToken)
        {
            try
            {
                var authSessions = await _authSessionRepo.GetAllAsync();
                var match = authSessions.FirstOrDefault(a => a.RefreshToken == refreshToken && a.IsDeleted == false);
                if (match == null) return false;
                await _authSessionRepo.DeleteOneAsync(match.Id);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, "[LogoutAsync] BUSINESS ERROR: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[LogoutAsync] ERROR: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<AuthResponse?> RefreshTokenAsync(string refreshToken)
        {
            try
            {
                var sessions = await _authSessionRepo.GetAllAsync();
                var match = sessions.FirstOrDefault(s => s.RefreshToken == refreshToken && s.ExpiresAt > DateTime.UtcNow && s.IsDeleted == false);
                if (match == null) return null;
                string accessToken;
                if (match.AdminId != null && match.AdminId > 0)
                {
                    var admin = await _adminLoginRepo.GetOneAsync(match.AdminId.Value);
                    if (admin == null) return null;
                    accessToken = _tokenService.GenerateAccessToken(admin.Id, admin.Email!.ToString(), admin.Role.ToString());
                }
                else if (match.EmployeeId != null && match.EmployeeId > 0)
                {
                    var employee = await _employeeRegisterRepo.GetOneAsync(match.EmployeeId.Value);
                    if (employee == null) return null;
                    accessToken = _tokenService.GenerateAccessToken(employee.Id, employee.Email!.ToString());
                }
                else
                {
                    return null;
                }

                return new AuthResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    ExpireAt = match.ExpiresAt
                };
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, "[RefreshTokenAsync] BUSINESS ERROR: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[RefreshTokenAsync] ERROR: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            try
            {
                var employees = await _employeeRegisterRepo.GetAllAsync();
                var admins = await _adminLoginRepo.GetAllAsync();

                if (employees.Any(e => e.Email == new EmailAddress(request.Email) && !e.IsDeleted) || admins.Any(a => a.Email == new EmailAddress(request.Email) && !a.IsDeleted))
                    throw new BusinessException("Email already exists.", 400);

                if (employees.Any(e => e.Phone == new PhoneNumber(request.Phone) && !e.IsDeleted))
                    throw new BusinessException("Phone number already exists.", 400);


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

                await _passwordResetTokenRepo.AddOneAsync(new PasswordResetToken
                {
                    EmpId = newEmployee.Id,
                    HashedToken = _tokenService.HashToken(token),
                    TokenType = TokenType.EmailVerification
                });

                await _emailSender.SendEmailAsync(
                    newEmployee.Email.ToString(),
                    "Verify Account Request",
                    $"Click the link to verify your account: {verifyLink}"
                );

                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, "[RegisterAsync] BUSINESS ERROR: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[RegisterAsync] ERROR: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordRequest request)
        {
            try
            {
                var tokens = await _passwordResetTokenRepo.GetAllAsync();
                var match = tokens.FirstOrDefault(t => t.HashedToken == _tokenService.HashToken(request.Token!) && !t.Used && t.ExpiresAt > DateTime.UtcNow && t.IsDeleted == false && t.TokenType == TokenType.PasswordReset);
                if (match == null) return false;
                if (match.AdminId != 0 && match.AdminId is not null)
                {
                    var admin = await _adminLoginRepo.GetOneAsync((int)match.AdminId);
                    if (admin == null) return false;
                    admin.PasswordHash = _passwordService.HashPassword(request.NewPassword!);
                    match.Used = true;
                    await _adminLoginRepo.UpdateOneAsync(admin);
                    await _passwordResetTokenRepo.UpdateOneAsync(match);
                    await _unitOfWork.SaveChangesAsync();
                    return true;
                }
                else if (match.EmpId != 0 && match.EmpId is not null)
                {
                    var employee = await _employeeRegisterRepo.GetOneAsync((int)match.EmpId);
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
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, "[ResetPasswordAsync] BUSINESS ERROR: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[ResetPasswordAsync] ERROR: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<AuthValidateResponse?> ValidateTokenAsync(string token)
        {
            try
            {
                var all = await _passwordResetTokenRepo.GetAllAsync();

                var match = all.FirstOrDefault(a => a.HashedToken == _tokenService.HashToken(token) && a.TokenType == TokenType.EmailVerification && a.ExpiresAt >= DateTime.UtcNow && a.IsDeleted == false && a.Used == false);
                if (match == null) return null;
                match.Used = true;

                var empMatch = await _employeeRegisterRepo.GetOneAsync((int)match.EmpId!);
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
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, "[ValidateTokenAsync] BUSINESS ERROR: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[ValidateTokenAsync] ERROR: {Message}", ex.Message);
                throw;
            }
        }
    }
}
