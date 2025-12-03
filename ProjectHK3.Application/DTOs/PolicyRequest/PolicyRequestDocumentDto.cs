namespace ProjectHK3.Application.DTOs.PolicyRequest
{
    public class PolicyRequestDocumentDto
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FileURL { get; set; } = string.Empty;
        public string DocumentType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int UploadedBy { get; set; }
    }
}
