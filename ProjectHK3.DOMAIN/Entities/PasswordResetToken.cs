using ProjectHK3.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHK3.Domain.Entities
{
    [Table("PasswordResetTokens")]
    public class PasswordResetToken : BaseEntity
    {
        public int? EmpId { get; set; }

        [ForeignKey(nameof(EmpId))]
        public EmpRegister? Employee { get; set; }

        public int? AdminId { get; set; }

        [ForeignKey(nameof(AdminId))]
        public AdminLogin? Admin { get; set; }

        [Column(TypeName ="VARCHAR(500)")]
        public string HashedToken { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddMinutes(10);

        public bool Used { get; set; } = false;

        public TokenType TokenType { get; set; }
    }

    public enum TokenType
    {
        EmailVerification = 0,
        PasswordReset = 1
    }

}
