using ProjectHK3.Domain.Common;
using ProjectHK3.Domain.Emtitys;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHK3.Domain.Entities
{
    [Table("AuthSession")]
    public class AuthSession : BaseEntity
    {
        public int? EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public EmpRegister? Employee { get; set; }

        public int? AdminId { get; set; }

        [ForeignKey(nameof(AdminId))]
        public AdminLogin? Admin { get; set; }

        public RoleOfAuthSession Role { get; set; }

        [Column(TypeName ="VARCHAR(255)")]
        public string? Token { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string? RefreshToken { get; set; }

        public DateTime ExpiresAt { get; set; }
    }

    public enum RoleOfAuthSession
    {
        Admin = 0,
        Manager = 1,
        Finance = 2,
        Employee = 3,
    }
}
