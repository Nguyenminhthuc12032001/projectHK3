namespace ProjectHK3.Api.Models.Finance
{
    public class CreateTransactionModel
    {
        public int ApprovalId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; } = string.Empty; // BankTransfer, Cash, ...
    }
}
