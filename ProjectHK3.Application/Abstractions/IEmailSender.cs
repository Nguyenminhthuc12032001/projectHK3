namespace ProjectHK3.Domain.Abstractions
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
