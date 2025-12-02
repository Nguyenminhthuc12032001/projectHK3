namespace ProjectHK3.Application.DTOs.PolicyRequest
{
    public class ListPolicyRequestDto
    {
        public int Id { get; set; }
        public string PolicyName { get; set; } = string.Empty;
        public string RequestType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateOnly RequestDate { get; set; }
    }
}
