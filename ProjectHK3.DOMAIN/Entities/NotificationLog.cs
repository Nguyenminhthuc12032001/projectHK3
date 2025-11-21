using ProjectHK3.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHK3.Domain.Entities
{
    internal enum TypeNotification
    {
        Email,
        SMS,
        InApp
    }
    internal enum StatusNotification
    {
        Sent,
        Failed,
        Queue
    }
    internal class NotificationLog : BaseEntity
    {
        [Column(TypeName = "varchar(150)")]
        public TypeNotification Type { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? Subject { get; set; }

        public string? Message { get; set; }
        public StatusNotification MyProperty { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime SentAt { get; set; }
    }
}
