using ProjectHK3.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHK3.Domain.Entities
{
    [Table("NotificationLog")]
    public class NotificationLog : BaseEntity
    {
        [Column(TypeName = "VARCHAR(150)")]
        public string? Recipient { get; set; }

        public TypeOfNotificationLog Type { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string? Subject { get; set; }

        [Column(TypeName = "TEXT")]
        public string? Message { get; set; }

        public bool IsRead { get; set; } = false;

        public StatusOfNotificationLog Status { get; set; }
    }
    public enum TypeOfNotificationLog
    {
        Email = 1,
        SMS = 2,
    }
    public enum StatusOfNotificationLog
    {
        Sent = 1,
        Failed = 2,
        Queue = 3,
    }
}
