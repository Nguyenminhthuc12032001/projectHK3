using ProjectHK3.Domain.Common;
using ProjectHK3.Domain.Emtitys;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHK3.Domain.Entities
{
    [Table("PolicyRequestDocuments")]
    public class PolicyRequestDocument : BaseEntity
    {
        public int RequestId { get; set; }

        [ForeignKey(nameof(RequestId))]
        public PolicyRequestDetail? RequestDetail { get; set; }

        [Column(TypeName ="VARCHAR(255)")]
        public string? FileName { get; set; }

        [Column(TypeName = "VARCHAR(500)")]
        public string? FileURL { get; set; }

        public DocumentTypeOfPolicyRequestDocument DocumentType { get; set; }

        public int UploadedBy { get; set; }

        [ForeignKey(nameof(UploadedBy))]
        public EmpRegister? Employee { get; set; }

        public StatusOfPolicyRequestDocument Status { get; set; }
    }

    public enum StatusOfPolicyRequestDocument
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2
    }

    public enum DocumentTypeOfPolicyRequestDocument
    {
        Invoice = 0,
        Medical = 1,
        IDCard = 2,
        Other = 3,
    }
}
