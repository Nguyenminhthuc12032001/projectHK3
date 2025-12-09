namespace ProjectHK3.Api.Models.Finance
{
    public class UpdateTransactionStatusModel
    {
        public string Status { get; set; } = string.Empty; // Completed | Failed | Reserved
    }
}
