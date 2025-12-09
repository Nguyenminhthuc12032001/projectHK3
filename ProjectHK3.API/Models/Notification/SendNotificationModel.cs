namespace ProjectHK3.Api.Models.Notification
{
    public class SendNotificationModel
    {
        public string Recipient { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // Email | SMS
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
