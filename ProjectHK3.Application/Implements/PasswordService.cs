using ProjectHK3.Application.Abstractions;

namespace ProjectHK3.Application.Implements
{
    public class PasswordService : IPasswordService
    {
        public string HashPassword(string plainPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainPassword);
        }

        public bool VerifyPassword(string hashedPassword, string plainPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
        }
    }
}
