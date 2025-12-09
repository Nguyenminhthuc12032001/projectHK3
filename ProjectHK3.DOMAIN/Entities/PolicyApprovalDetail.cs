using ProjectHK3.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHK3.Domain.Entities
{
    [Table("PolicyApprovalDetails")]
    public class PolicyApprovalDetail : BaseEntity
    {
        public int RequestId { get; set; }

        [ForeignKey(nameof(RequestId))]
        public PolicyRequestDetail? PolicyRequestDetail { get; set; }
        
        public int AdminId { get; set; }

        [ForeignKey(nameof(AdminId))]
        public AdminLogin? Admin { get; set; }

        [Column(TypeName = "DATE")]
        public DateOnly ApprovalDate { get; set; }

        [Column(TypeName = "TEXT")]
        public string? Remarks { get; set; }

        public StatusOfPolicyApprovalDetail Status { get; set; }

        public PaymentStatusOfPolicyApprovalDetail PaymentStatus { get; set; }
    }

    public enum StatusOfPolicyApprovalDetail
    {
        Approved = 0,
        Rejected = 1,
        NeedInfo = 2,
    }

    public enum PaymentStatusOfPolicyApprovalDetail
    {
        Unpaid = 0,
        Paid = 1,
        Refunded = 2,
    }
}
