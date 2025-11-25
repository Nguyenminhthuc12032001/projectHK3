using ProjectHK3.Domain.Common;
using ProjectHK3.Domain.Emtitys;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHK3.Domain.Entities
{
    [Table("PoliciesOnEmployees")]
    public class PoliciesOnEmployee : BaseEntity
    {
        public int EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public EmpRegister? Employee { get; set; }

        public int PolicyId { get; set; }

        [ForeignKey(nameof(PolicyId))]
        public Policy? Policy { get; set; }

        [Column(TypeName = "DATE")]
        public DateOnly StartDate { get; set; }

        [Column(TypeName = "DATE")]
        public DateOnly EndDate { get; set; }

        public StatusOfPoliciesOnEmployee Status { get; set; }
    }

    public enum StatusOfPoliciesOnEmployee
    {
        Active = 0,
        Expired = 1,
        Cancelled = 2,
    }
}
