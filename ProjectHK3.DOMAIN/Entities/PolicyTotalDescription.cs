using ProjectHK3.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHK3.Domain.Entities
{
    [Table("PolicyTotalDescription")]
    public class PolicyTotalDescription : BaseEntity
    {
        public int PolicyId { get; set; }

        [ForeignKey(nameof(PolicyId))]
        public Policy? Policy { get; set; }

        [Column(TypeName = "TEXT")]
        public string? PolicySummary { get; set; }

        [Column(TypeName = "TEXT")]
        public string? Benefits { get; set; }

        [Column(TypeName = "TEXT")]
        public string? Exclutions { get; set; }

        [Column(TypeName = "TEXT")]
        public string? TermsConditions { get; set; }
    }
}
