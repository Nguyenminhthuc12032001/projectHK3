using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectHK3.Domain.Common;



namespace ProjectHK3.Domain.Entities
{

    public enum MethodTransaction
    {
        BankTransfer,
        Ewallet,
        Cash,
        Refund
    }

    public enum StatusTransaction
    {
        Pending,
        Completed,
        Failed,
        Reserved
    }
    internal class TransactionLedger : BaseEntity
    {


        public int ApprovalId { get; set; }


        public PolicyApprovalDetails Approval { get; set; }


        //Decimal (12,2)
        [Column(TypeName = "decimal(12,2)")]
        public decimal Amount { get; set; }


        public MethodTransaction Method { get; set; }

        public StatusTransaction Status { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Timestamp { get; set; }


    }
}
