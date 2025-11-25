using ProjectHK3.Domain.Common;
using ProjectHK3.Domain.Entities;
using ProjectHK3.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHK3.Domain.Emtitys
{
    [Table("EmpRegister")]
    public class EmpRegister : BaseEntity
    {
        [Column(TypeName ="VARCHAR(150)")]
        public string? FullName { get; set; }

        [Column(TypeName ="VARCHAR(150)")]
        public string? Email { get; set; }

        [NotMapped]
        public PhoneNumber? Phone { get; set; }

        [Column(nameof(Phone), TypeName = "VARCHAR(20)")]
        public string? PhoneValue
        {
            get => Phone?.Value;
            set => Phone = new PhoneNumber(value);
        }

        [Column(TypeName ="VARCHAR(255)")]
        public string? PasswordHash { get; set; }

        [Column(TypeName ="VARCHAR(100)")]
        public string? Department { get; set; }

        [Column(TypeName = "DATE")]
        public DateOnly HireDate { get; set; }

        public string? Status { get; set; }

        public int CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public CompanyDetail? Company {  get; set; }
    }

    public enum StatusOfEmpRegister
    {
        Active = 0,
        Inactive = 1,
    }
}
