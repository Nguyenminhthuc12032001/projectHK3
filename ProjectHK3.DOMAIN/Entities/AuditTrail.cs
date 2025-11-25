using ProjectHK3.Domain.Common;
using ProjectHK3.Domain.Emtitys;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProjectHK3.Domain.Entities
{
    public class AuditTrail : BaseEntity
    {
        public int EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public EmpRegister? Employee { get; set; }

        public int AdminId { get; set; }

        [ForeignKey(nameof(AdminId))]
        public AdminLogin? Admin { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        public string? Action { get; set; }

        [Column(TypeName = ("VARCHAR(100)"))]
        public string? TableName { get; set; }

        public int RecordId { get; set; }

        [Column(TypeName = "TEXT")]
        public string? Details { get; set; }
    }
}
