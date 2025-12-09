using ProjectHK3.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHK3.Domain.Entities
{
    [Table("PolicyRequestDetails")]
    public class PolicyRequestDetail : BaseEntity
    {
        public int EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public EmpRegister? Employee { get; set; }

        public int PolicyId { get; set; }

        [ForeignKey(nameof(PolicyId))]
        public Policy? Policy { get; set; }

        public RequestTypeOfPolicyRequestDetail RequestType {  get; set; }

        [Column(TypeName = "DATE")]
        public DateOnly RequestDate { get; set; }

        public StatusOfPolicyRequestDetail Status { get; set; }

        [Column(TypeName = "TEXT")]
        public string? Remarks { get; set; }
    }

    public enum RequestTypeOfPolicyRequestDetail
    {
        Enrollment = 0,
        Claim = 1,
        Cancel = 2,
    }

    public enum StatusOfPolicyRequestDetail
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2,
        NeedInfo = 3
    }
}
