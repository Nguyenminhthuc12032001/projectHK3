using ProjectHK3.Domain.Common;
using ProjectHK3.Domain.Emtitys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHK3.Domain.Entities
{
    internal class AuditTrail : BaseEntity
    {
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User? GetUserId { get; set; }

        [Column(TypeName = ("varchar(50)"))]

        public string? Action { get; set; }

        [Column(TypeName = ("varchar(100)"))]

        public string TableName { get; set; }

        public int RecordId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Timestamp { get; set; }

        public string? Details { get; set; }
    }
}
