namespace ProjectHK3.Application.DTOs.Hospital
{
    public class DetailHospital
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;

        public string? HospitalName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? ContactPhone { get; set; }
        public string? Email { get; set; }
        public DateOnly RegisteredOn { get; set; }
    }
}
