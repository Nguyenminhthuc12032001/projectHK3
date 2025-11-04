namespace ProjectHK3.Domain.Abstractions
{
    internal interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
