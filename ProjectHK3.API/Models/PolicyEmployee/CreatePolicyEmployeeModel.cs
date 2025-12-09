namespace ProjectHK3.Api.Models.PolicyEmployee
{
    public class CreatePolicyEmployeeModel
    {
        public int EmployeeId { get; set; }
        public int PolicyId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
