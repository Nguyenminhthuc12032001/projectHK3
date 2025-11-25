namespace ProjectHK3.Domain.Common
{
    public class BaseEntity : IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime UpdatedBy { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
