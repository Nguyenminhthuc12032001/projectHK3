using ProjectHK3.Domain.Common;
using ProjectHK3.Domain.Emtitys;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHK3.Domain.Entities
{
    [Table("ReportLogs")]
    public class ReportLog : BaseEntity
    {
        [Column(TypeName = "VARCHAR(100)")]
        public  string? ReportType { get; set; }

        public int GeneratedBy { get; set; }

        [ForeignKey(nameof(GeneratedBy))]
        public AdminLogin? Admin { get; set; }
    }
}
