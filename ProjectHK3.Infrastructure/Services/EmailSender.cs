using Microsoft.Extensions.Options;
using ProjectHK3.Application.Settings;
using ProjectHK3.Domain.Abstractions;
using System.Net;
using System.Net.Mail;

namespace ProjectHK3.Infrastructure.Services
{
    public class EmailSender(IOptions<SmtpSettings> smtpSettings) : IEmailSender
    {
        private readonly SmtpSettings _smtpSettings = smtpSettings.Value;
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using var client = new SmtpClient(_smtpSettings.Host)
            {
                Port = _smtpSettings.Port,
                EnableSsl = _smtpSettings.EnableSsl,
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpSettings.From),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(new MailAddress(to));

            await client.SendMailAsync(mailMessage);
        }
    }
}
