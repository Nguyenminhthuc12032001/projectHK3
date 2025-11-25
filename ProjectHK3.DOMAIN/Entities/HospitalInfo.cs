using ProjectHK3.Domain.Common;
using ProjectHK3.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHK3.Domain.Entities
{
    [Table("HospitalInfo")]
    public class HospitalInfo : BaseEntity
    {
        [Column(TypeName = "VARCHAR(200)")]
        public string? HospitalName { get; set; }

        [Column(TypeName = "VARCHAR(100)")]
        public string? Address { get; set; }

        [Column(TypeName = "TEXT")]
        public string? City { get; set; }

        [NotMapped]
        public PhoneNumber? ContactPhone { get; set; }

        [Column(nameof(ContactPhone), TypeName ="VARCHAR(20)")]
        public string? PhoneValue
        {
            get => ContactPhone?.Value;
            set => ContactPhone = new PhoneNumber(value);
        }

        [NotMapped]
        public EmailAddress? Email {  get; set; }

        [Column(nameof(Email), TypeName = "VARCHAR(150)")]
        public string? EmailValue
        {
            get => Email?.Value;
            set => Email = new EmailAddress(value);
        }

        [Column(TypeName = "VARCHAR(255)")]
        public string? ApiKey { get; set; }

        [Column(TypeName = "DATE")]
        public DateOnly RegisteredOn { get; set; }
    }
}
