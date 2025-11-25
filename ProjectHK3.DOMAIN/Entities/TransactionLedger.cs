using System.ComponentModel.DataAnnotations.Schema;
using ProjectHK3.Domain.Common;

namespace ProjectHK3.Domain.Entities
{
    [Table("TransactionLedgers")]
    public class TransactionLedger : BaseEntity
    {
        public int ApprovalId { get; set; }

        [ForeignKey(nameof(ApprovalId))]
        public PolicyApprovalDetail? Approval { get; set; }

        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal Amount { get; set; }

        public MethodOfTransactionLedger Method { get; set; }

        public StatusOfTransactionLedger Status { get; set; }
    }

    public enum MethodOfTransactionLedger
    {
        BankTransfer = 0,
        Ewallet = 1,
        Cash = 2,
        Refund = 3
    }

    public enum StatusOfTransactionLedger
    {
        Pending = 0,
        Completed = 1,
        Failed = 2,
        Reserved= 3,
    }
}
