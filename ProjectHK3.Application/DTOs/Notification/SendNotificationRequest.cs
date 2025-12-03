using ProjectHK3.Domain.Entities;

namespace ProjectHK3.Application.DTOs.Notification
{
    public class SendNotificationRequest
    {
        public string Subject { get; internal set; }
        public TypeOfNotificationLog Type { get; internal set; }
        public string Recipient { get; internal set; }
        public string Message { get; internal set; }
    }
}
