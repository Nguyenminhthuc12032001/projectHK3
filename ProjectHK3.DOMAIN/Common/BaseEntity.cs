using System.ComponentModel.DataAnnotations;

namespace ProjectHK3.Domain.Common
{
    public class BaseEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
