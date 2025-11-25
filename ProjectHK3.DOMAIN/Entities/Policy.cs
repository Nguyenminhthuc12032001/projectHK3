using ProjectHK3.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHK3.Domain.Entities
{
    [Table("Policies")]
    public class Policy : BaseEntity
    {
        public int CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public CompanyDetail? Company { get; set; }

        [Column(TypeName = "VARCHAR(200)")]
        public string? PolicyName { get; set; }

        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal CoverageAmount { get; set; }

        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal PremiumAmount { get; set; }

        [Column(TypeName = "DATE")]
        public DateOnly EffectiveFrom {  get; set; }

        [Column(TypeName = "DATE")]
        public DateOnly EffectiveTo { get; set; }

        public StatusOfPolicy Status { get; set; }
    }

    public enum StatusOfPolicy
    {
        Draft = 0,
        Active = 1,
        Expired = 2,
    }
}
