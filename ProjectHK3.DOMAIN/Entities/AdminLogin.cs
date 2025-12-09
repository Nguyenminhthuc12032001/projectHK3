using ProjectHK3.Domain.Common;
using ProjectHK3.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHK3.Domain.Entities
{
    [Table("AdminLogin")]
    public class AdminLogin : BaseEntity
    {
        [Column(TypeName = "VARCHAR(100)")]
        public string? UserName { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string? PasswordHash { get; set; }

        [NotMapped]
        public EmailAddress? Email { get; set; }

        [Column("Email", TypeName = "VARCHAR(150)")]
        public string? EmailValue
        {
            get => Email?.Value;
            set => Email = new EmailAddress(value);
        }

        public RoleOfAdminLogin Role { get; set; }

        public StatusOfAdminLogin Status { get; set; }
    }

    public enum StatusOfAdminLogin
    {
        Active = 0,
        Suspended = 1,
    }

    public enum RoleOfAdminLogin
    {
        Admin = 0,
        Manager = 1,
        Finance = 2,
    }
}
