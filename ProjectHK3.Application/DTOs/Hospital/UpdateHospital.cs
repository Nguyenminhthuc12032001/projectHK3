namespace ProjectHK3.Application.DTOs.Hospital
{
    public class UpdateHospital
    {
        public int Id { get; set; }

        public string? HospitalName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? ContactPhone { get; set; }
        public string? Email { get; set; }
    }
}
