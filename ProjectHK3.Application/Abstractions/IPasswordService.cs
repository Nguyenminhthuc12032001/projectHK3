namespace ProjectHK3.Application.Abstractions
{
    public interface IPasswordService
    {
        string HashPassword(string plainPassword);
        bool VerifyPassword(string hashedPassword, string plainPassword);
    }
}
