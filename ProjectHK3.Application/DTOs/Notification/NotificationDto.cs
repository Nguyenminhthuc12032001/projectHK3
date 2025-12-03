
namespace ProjectHK3.Application.DTOs.Notification
{
    public class NotificationDto
    {
        public int Id { get; internal set; }
        public string? Recipient { get; internal set; }
        public string Type { get; internal set; }
        public string? Subject { get; internal set; }
        public string? Message { get; internal set; }
        public bool IsRead { get; internal set; }
        public string Status { get; internal set; }
        public DateTimeOffset CreatedAt { get; internal set; }
    }
}
