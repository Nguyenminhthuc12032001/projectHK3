using ProjectHK3.Domain.Common;
using ProjectHK3.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHK3.Domain.Entities
{
    [Table("CompanyDetails")]
    public class CompanyDetail : BaseEntity
    {
        [Column(TypeName = "VARCHAR(200)")]
        public string? CompanyName { get; set; }

        [Column(TypeName = "TEXT")]
        public string? Address { get; set; }

        [NotMapped]
        public EmailAddress? ContactEmail { get; set; }

        [NotMapped]
        public PhoneNumber? ContactPhone { get; set; }

        [Column(TypeName = "VARCHAR(150)")]
        public string? Website { get; set; }

        [Column(nameof(ContactEmail) ,TypeName = "VARCHAR(150)")]
        public string? EmailValue
        {
            get => ContactEmail?.Value;
            set => ContactEmail = new EmailAddress(value);
        }

        [Column(nameof(ContactPhone), TypeName = "VARCHAR(20)")]
        public string? PhoneValue
        {
            get => ContactPhone?.Value;
            set => ContactPhone = new PhoneNumber(value);
        }

        public int? CreateBy {  get; set; }

        [ForeignKey(nameof(CreateBy))]
        public AdminLogin? Admin { get; set; }
    }
}
