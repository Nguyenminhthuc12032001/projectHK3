using ProjectHK3.Domain.Common;
using ProjectHK3.Domain.Emtitys;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHK3.Domain.Entities
{
    [Table("PoliciesOnEmployees")]
    public class PoliciesOnEmployees : BaseEntity
    {
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public EmpRegister? Employee { get; set; }

        public int PolicyId { get; set; }

        [ForeignKey("PolicyId")]
        public Policy? Policy { get; set; }

        [Column(TypeName = "DATE")]
        public DateOnly StartDate { get; set; }

        [Column(TypeName = "DATE")]
        public DateOnly EndDate { get; set; }

        [Column(TypeName = "ENUM('Active','Expired','Cancelled')")]
        public string? Status { get; set; }
    }
}
