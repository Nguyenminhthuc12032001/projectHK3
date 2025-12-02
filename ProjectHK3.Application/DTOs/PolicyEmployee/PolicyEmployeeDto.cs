namespace ProjectHK3.Application.DTOs.PolicyEmployee
{
    public class PolicyEmployeeDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int PolicyId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
