namespace ProjectHK3.Api.Models.Notification
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public string? Recipient { get; set; }
        public string Type { get; set; } = string.Empty;
        public string? Subject { get; set; }
        public string? Message { get; set; }
        public bool IsRead { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; }
    }
}
